using Microsoft.EntityFrameworkCore;
using NichiOnlineTest.Common;
using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.Core.Logic
{
    public class OnlineTestCoreLogic
    {
        private readonly VinayTestDBContext _context;

        public OnlineTestCoreLogic()
        {
            _context = new VinayTestDBContext();
            _context.Database.SetCommandTimeout(120000);
        }
        public OnlineTestDataModel GetOnlineTestQuestions(string userId)
        {
            var category = _context.NisCategory.Where(o => o.IsPublished).OrderByDescending(o => o.UpdatedDate).FirstOrDefault();

            if (category == null)
                throw new Exception(Messages.OT_0018);

            var subcategories = _context.NisSubcategory.Where(o => o.CategoryId == category.Id).Select(o => new SubCategory
            {
                SubCategoryId = o.Id,
                SubCategoryName = o.Name
            }).ToList();

            var totalQuestions = 0;
            var attendedQuestion = 0;


            foreach (var item in subcategories)
            {
                item.Questions = new List<Questions>();
                var questionsList = _context.NisQuestions
                                            .Where(o => o.SubcategoryId == item.SubCategoryId).Select(o => new Questions
                                            {
                                                QuestionId = o.Id,
                                                QuestionType = o.Type,
                                                QuestionText = o.Question,
                                                QuestionImg = o.QuestionImage,
                                                AnswerImg = o.AnswerImage,
                                                IsMultiple = o.IsMultipleAnswer,
                                            });

                if (category.IsShuffleQuestions)
                    item.Questions = questionsList.OrderBy(m => Guid.NewGuid()).ToList();
                else
                    item.Questions = questionsList.ToList();

                totalQuestions += item.Questions.Count;
                item.TotalQuestions = item.Questions.Count;

                foreach (var question in item.Questions)
                {
                    question.Answers = new List<Answers>();
                    var userAnswers = _context.NisUserAnswers
                        .Where(o => o.QuestionId == question.QuestionId && o.UserId == userId)
                        .ToList();
                    question.Answers = _context.NisQuestionAnswers.Where(o => o.QuestionId == question.QuestionId).OrderBy(o => o.Order).Select(o => new Answers
                    {
                        AnswerId = o.Id,
                        AnswerText = o.Answer,
                        Marks = o.Marks,
                    }).ToList();

                    if (userAnswers != null && userAnswers.Count > 0)
                    {
                        foreach (var userans in userAnswers)
                        {
                            var temp = question.Answers.SingleOrDefault(o => o.AnswerId.ToString() == userans.Question_Answers );

                            if (temp != null)
                            {
                                temp.IsSelected = question.QuestionType == 1 ? true : false;
                                temp.UserAnswer = question.QuestionType == 1 ? "" : userans.AnswerText;
                                question.IsAnswered = true;
                            }
                        }
                    }

                    if (question.IsAnswered)
                    {
                        item.AttendedQuestions++;
                        attendedQuestion++;
                    }
                }
            }

            var time = category.Time;

            var actitivity = _context.NisUserTestActivity.SingleOrDefault(o => o.CategoryId == category.Id && o.UserId == userId);
            if (actitivity != null)
            {
                time = actitivity.RunningDateTime.Subtract(actitivity.StartDateTime);
                time = category.Time - time;
            }
            else
            {
                StartTest(category.Id, userId);
            }


            return new OnlineTestDataModel
            {
                IsTestSubmitted = actitivity?.EndDateTime != null ? true : false,
                CategoryId = category.Id,
                CategoryName = category.Name,
                Time = time,
                TotalQuestions = totalQuestions,
                SubCategories = subcategories,
                UserId = userId,
                AttendedQuestions = attendedQuestion
            };

        }

        public void StartTest(Guid categoryId, string userId)
        {
            _context.NisUserTestActivity.Add(new NisUserTestActivity
            {
                CategoryId = categoryId,
                Id = Guid.NewGuid(),
                StartDateTime = DateTime.Now,
                RunningDateTime = DateTime.Now,
                UserId = userId,
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.Parse(userId)
            });

            _context.SaveChanges();
        }

        public void UpdateUserActivity(Guid categoryId, string userId)
        {
            var activity = _context.NisUserTestActivity.SingleOrDefault(o => o.CategoryId == categoryId && o.UserId == userId);
            if (activity != null)
            {
                activity.RunningDateTime = DateTime.Now;
            }

            _context.SaveChanges();
        }
        public void SubmitAnswers(OnlineTestDataModel model)
        {
            var todayDate = DateTime.Now;
            var trans = _context.Database.BeginTransaction();

            var deletePrevAnswers = _context.NisUserAnswers.Where(o => o.Question.CategoryId == model.CategoryId && o.UserId == model.UserId);

            _context.NisUserAnswers.RemoveRange(deletePrevAnswers);

            _context.SaveChanges();


            foreach (var item in model.SubCategories)
            {
                foreach (var question in item.Questions)
                {
                    foreach (var answer in question.Answers)
                    {
                        if (answer.IsSelected || !string.IsNullOrEmpty(answer.UserAnswer))
                        {
                            _context.NisUserAnswers.Add(new NisUserAnswers
                            {
                                Id = Guid.NewGuid(),
                                AnswerText = question.QuestionType == 2 ? answer.UserAnswer : null,
                                Question_Answers = answer.AnswerId.ToString(),
                                QuestionId = question.QuestionId,
                                UserId = model.UserId,
                                CreatedDate = todayDate,
                                CreatedBy = Guid.Parse(model.UserId)
                            });
                        }
                    }
                }
            }

            _context.SaveChanges();

            var activity = _context.NisUserTestActivity.SingleOrDefault(o => o.CategoryId == model.CategoryId && o.UserId == model.UserId);
            if (activity != null)
            {
                activity.RunningDateTime = todayDate;
                if (model.IsTestSubmitted)
                {
                    activity.EndDateTime = todayDate;
                }
            }

            _context.SaveChanges();

            trans.Commit();

        }
    }
}
