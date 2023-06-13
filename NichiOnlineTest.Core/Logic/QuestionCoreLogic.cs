using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.Common;

namespace NichiOnlineTest.Core.Logic
{
    public class QuestionCoreLogic
    {
        /// <summary>
        /// Get questions based on category and subcategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="subCategoryId"></param>
        /// <returns></returns>
        public QuestionMasterDataModel GetQuestionsBySubCategoryId(Guid categoryId, Guid subCategoryId)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var allQuestionsFromPublished = dbContext.NisQuestions
                                                             .Where(x => x.SubcategoryId == subCategoryId)
                                                             .ToList();

                    var isCategoryPublished = dbContext.NisCategory
                                                       .SingleOrDefault(x => x.Id == categoryId).IsPublished;

                    var questionDataModel = new List<QuestionDataModel>();

                    foreach (var question in allQuestionsFromPublished)
                    {
                        var answerInQuestion = dbContext.NisQuestionAnswers
                                                        .Where(x => x.QuestionId == question.Id)
                                                        .Select(m => new AnswerDataModel()
                                                        {
                                                            Id = m.Id,
                                                            ForQuestionId = m.QuestionId,
                                                            Text = m.Answer,
                                                            Marks = m.Marks,
                                                            IsCorrectAnswer = m.IsCorrectAnswer,
                                                            Order = m.Order
                                                        })
                                                        .OrderBy(x => x.Order)
                                                        .ToList();

                        questionDataModel.Add(new QuestionDataModel()
                        {
                            Id = question.Id,
                            CategoryId = question.CategoryId,
                            SubCategoryId = question.SubcategoryId,
                            Title = question.Question,
                            Type = question.Type,
                            // Marks = question.Type == 1 ? answerInQuestion.FirstOrDefault(x => x.IsCorrectAnswer).Marks : answerInQuestion.Sum(x => x.Marks),
                            Marks = answerInQuestion.Sum(x => x.Marks),
                            QuestionImageURL = question.QuestionImage,
                            AnswerImageURL = question.AnswerImage,
                            IsMultipleAnswer = question.IsMultipleAnswer,
                            Answers = answerInQuestion,
                            CreatedBy = question.CreatedBy,
                            UpdatedBy = question.UpdatedBy.HasValue ? question.UpdatedBy.Value : Guid.Empty
                        });
                    }

                    var dataModel = new QuestionMasterDataModel()
                    {
                        CategoryId = categoryId,
                        SubCategoryId = subCategoryId,
                        IsCategoryPublished = isCategoryPublished,
                        Questions = questionDataModel
                    };

                    dataModel.CategoryName = dbContext.NisCategory.SingleOrDefault(x => x.Id == dataModel.CategoryId).Name;
                    dataModel.SubCategoryName = dbContext.NisSubcategory.SingleOrDefault(x => x.Id == dataModel.SubCategoryId).Name;

                    return dataModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<SubCategorySummary> GetSubCategorySummaryByCategoryId(Guid categoryId)
        {
            using (var dbContext = new VinayTestDBContext())
            {

                var result = (from category in dbContext.NisCategory
                              join subcategory in dbContext.NisSubcategory on category.Id equals subcategory.CategoryId
                              where category.Id == categoryId
                              select new SubCategorySummary
                              {
                                  CategoryId = category.Id,
                                  CategoryName = category.Name,
                                  SubCategoryId = subcategory.Id,
                                  SubCategoryName = subcategory.Name,
                                  SubCategoryMarks = subcategory.Marks,
                                  CategoryMarks = category.TotalMarks,
                                  IsCategoryPublished = category.IsPublished
                              }).ToList();

                return result;
            }
        }

        /// <summary>
        /// create a new set of question and answer
        /// </summary>
        /// <param name="questionDataModel"></param>
        /// <returns></returns>
        public bool Create(QuestionMasterDataModel questionDataModel)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var questionsList = new List<NisQuestions>();
                    var answersList = new List<NisQuestionAnswers>();

                    foreach (var question in questionDataModel.Questions)
                    {
                        questionsList.Add(new NisQuestions()
                        {
                            Id = question.Id,
                            CategoryId = question.CategoryId,
                            SubcategoryId = question.SubCategoryId,
                            Question = question.Title,
                            //Marks = question.Marks,
                            Type = question.Type,
                            QuestionImage = question.Type == 2 ? question.QuestionImageURL : null,
                            AnswerImage = question.Type == 2 ? question.AnswerImageURL : null,
                            IsMultipleAnswer = question.IsMultipleAnswer,
                        });

                        foreach (var answer in question.Answers)
                        {
                            answersList.Add(new NisQuestionAnswers()
                            {
                                Id = answer.Id,
                                QuestionId = answer.ForQuestionId,
                                Answer = answer.Text,
                                Marks = answer.Marks,
                                Order = answer.Order,
                                IsCorrectAnswer = answer.IsCorrectAnswer,
                            });
                        }
                    }

                    dbContext.NisQuestions.AddRange(questionsList);
                    dbContext.NisQuestionAnswers.AddRange(answersList);

                    dbContext.SaveChanges();

                    var sumOfSubCategory = (from qa in dbContext.NisQuestionAnswers
                                            join q in dbContext.NisQuestions on qa.QuestionId equals q.Id
                                            where q.SubcategoryId == questionDataModel.SubCategoryId
                                            select qa.Marks).Sum();

                    var subCategory = dbContext.NisSubcategory.SingleOrDefault(x => x.Id == questionDataModel.SubCategoryId);

                    subCategory.Marks = sumOfSubCategory;

                    dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public bool Update(QuestionMasterDataModel questionDataModel)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var toDeleteQuestions = dbContext.NisQuestions.Where(x => x.SubcategoryId == questionDataModel.SubCategoryId);

                    var toDelete = toDeleteQuestions.Select(x => x.Id);
                    var toDeleteAnswers = dbContext.NisQuestionAnswers.Where(x => toDelete.Contains(x.QuestionId));

                    dbContext.NisQuestions.RemoveRange(toDeleteQuestions);
                    dbContext.NisQuestionAnswers.RemoveRange(toDeleteAnswers);

                    dbContext.SaveChanges();
                }

                return Create(questionDataModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Publish category if sub category marks equals category marks
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool Publish(Guid categoryId)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var category = dbContext.NisCategory.SingleOrDefault(x => x.Id == categoryId);

                    var subCategories = dbContext.NisSubcategory.Where(x => x.CategoryId == categoryId);

                    var subCategoryMarks = subCategories.Sum(x => x.Marks);

                    if (subCategories.Any(x => x.Marks == 0))
                    {
                        throw new Exception(Messages.OT_0019);
                    }
                    else if (subCategoryMarks == category.TotalMarks)
                    {
                        category.IsPublished = true;

                        dbContext.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception(Messages.OT_0013);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
