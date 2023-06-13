using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.Core.Logic
{
    public class ShowResultCoreLogic
    {
        private VinayTestDBContext _context;
        public ShowResultCoreLogic()
        {
            _context = new VinayTestDBContext();
        }
        public ShowResultDataModel GetResultByUser(Guid categoryId, string userName)
        {
            try
            {
                var category = _context.NisCategory.SingleOrDefault(x => x.Id == categoryId);
                var user = _context.NisUsers.FirstOrDefault(x => x.UserName == userName);

                var subCategoryList = _context.NisSubcategory.Where(x => x.CategoryId == categoryId).ToList();
                var subcategoryDataModel = new List<ShowResultSubcategoryDataModel>();

                foreach (var subcategory in subCategoryList)
                {
                    var questionDataModel = new List<ShowResultQuestionDataModel>();
                    var allQuestions = _context.NisQuestions.Where(n => n.SubcategoryId == subcategory.Id && n.CategoryId == categoryId).ToList();
                    foreach (var question in allQuestions)
                    {
                        var userAnswer = _context.NisUserAnswers.Where(x => x.QuestionId == question.Id && x.UserId == user.Id);

                        var answerInQuestion = _context.NisQuestionAnswers
                            .Where(x => x.QuestionId == question.Id)
                            .OrderByDescending(x => x.CreatedDate)
                            .Select(m => new ShowResultAnswerDataModel()
                            {
                                Id = m.Id,
                                ForQuestionId = m.QuestionId,
                                Text = m.Answer,
                                Marks = m.Marks,
                                IsCorrectAnswer = m.IsCorrectAnswer,
                                Order = m.Order,
                                //ForUserAnswerId = userAnswer.Where(n => n.QuestionAnswers == m.Id) == null ?
                                //                            Guid.Empty :
                                //                           userAnswer.FirstOrDefault(n => n.QuestionAnswers == m.Id).QuestionAnswers,
                                ForUserAnswerId = question.Type == 1
                                    ? (m.Id.ToString() == userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).Question_Answers ? Guid.Parse(userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).Question_Answers) : Guid.Empty)
                                    : (m.Answer == userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).AnswerText ? Guid.Parse(userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).Question_Answers) : Guid.Empty),
                                IsUserCorrectAnswer = question.Type == 1
                                                                    ? (m.Id.ToString() == userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).Question_Answers ? true : false)
                                                                    : (m.Answer == userAnswer.FirstOrDefault(x => x.Question_Answers == m.Id.ToString()).AnswerText ? true : false),

                                Text2 = question.Type == 2 ? userAnswer.SingleOrDefault(x => x.Question_Answers == m.Id.ToString()).AnswerText : null,


                            })
                            .OrderBy(x => x.Order)
                            .ToList();

                        questionDataModel.Add(new ShowResultQuestionDataModel()
                        {
                            Id = question.Id,
                            CategoryId = question.CategoryId,
                            SubCategoryId = question.SubcategoryId,
                            Title = question.Question,
                            Type = question.Type,
                            Marks = answerInQuestion.Sum(x => x.Marks),
                            QuestionImageURL = question.QuestionImage,
                            AnswerImageURL = question.AnswerImage,
                            IsMultipleAnswer = question.IsMultipleAnswer,
                            AnswerOptions = answerInQuestion
                        });
                    }

                    subcategoryDataModel.Add(new ShowResultSubcategoryDataModel
                    {
                        SubCategoryName = subcategory.Name,
                        SubcategoryId = subcategory.Id,
                        Marks = subcategory.Marks,
                        Questions = questionDataModel
                    });

                }

                var showResultList = new ShowResultDataModel
                {
                    CategoryName = category.Name,
                    CandidateName = user.FirstName + " " + user.LastName,
                    TotalMarks = category.TotalMarks,
                    SubcategoryList = subcategoryDataModel
                };

                return showResultList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
