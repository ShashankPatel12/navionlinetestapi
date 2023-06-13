using NichiOnlineTest.Common;
using NichiOnlineTest.Core.Data;
using NichiOnlineTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.Core.Logic
{
    public class CategoryCoreLogic
    {
        private readonly VinayTestDBContext _context;

        public CategoryCoreLogic()
        {
            _context = new VinayTestDBContext();
        }

        public List<CategoryDataModel> GetAllCategory()
        {
            return _context.NisCategory.Select(x => new CategoryDataModel
            {
                CategoryId = x.Id,
                Name = x.Name,
                TotalTime = x.Time,
                IsPublished = x.IsPublished,
                IsShuffle = x.IsShuffleQuestions,
                TotalCategoryMarks = x.TotalMarks,
                CreatedOn = x.CreatedDate,
                UpdatedOn = x.UpdatedDate
            }).ToList();
        }

        public CategoryDataModel GetCategoryById(Guid id)
        {
            var categoryById = _context.NisCategory.FirstOrDefault(x => x.Id == id);
            CategoryDataModel model = new CategoryDataModel
            {
                CategoryId = categoryById.Id,
                Name = categoryById.Name,
                TotalTime = categoryById.Time,
                IsPublished = categoryById.IsPublished,
                IsShuffle = categoryById.IsShuffleQuestions,
                TotalCategoryMarks = categoryById.TotalMarks
            };
            return model;
        }

        public bool CreateCategory(CategoryDataModel categoryDataModel)
        {
            try
            {
                var subCategoryList = new List<NisSubcategory>();
                var questionsList = new List<NisQuestions>();
                var answersList = new List<NisQuestionAnswers>();

                using (var dbContext = new VinayTestDBContext())
                {
                    var isCategoryNotPublished = dbContext.NisCategory.SingleOrDefault(x => !x.IsPublished);

                    if (isCategoryNotPublished != null)
                        throw new Exception(Messages.OT_0020);

                    if (!IsCategoryNameExists(categoryDataModel.Name))
                    {
                        var category = new NisCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = categoryDataModel.Name,
                            Time = categoryDataModel.TotalTime,
                            IsPublished = categoryDataModel.IsPublished,
                            IsShuffleQuestions = categoryDataModel.IsShuffle,
                            TotalMarks = categoryDataModel.TotalCategoryMarks
                        };

                        foreach (var subcategory in categoryDataModel.SubCategoryGroup)
                        {
                            var subCategoryId = Guid.NewGuid();

                            var questionDataModel = (from question in dbContext.NisQuestions
                                                     where (question.CategoryId == subcategory.CategoryId) &&
                                                           (question.SubcategoryId == subcategory.SubCategoryId)
                                                     select new
                                                     {
                                                         question.Id,
                                                         question.Question,
                                                         question.Type,
                                                         question.QuestionImage,
                                                         question.AnswerImage,
                                                         question.IsMultipleAnswer
                                                     }).ToList();

                            subCategoryList.Add(new NisSubcategory()
                            {
                                Id = subCategoryId,
                                Name = subcategory.SubCategoryName,
                                CategoryId = category.Id,
                                ClonedId = subcategory.SubCategoryId,
                                Marks = subcategory.Marks
                            });

                            foreach (var question in questionDataModel)
                            {
                                var questionsId = Guid.NewGuid();

                                questionsList.Add(new NisQuestions()
                                {
                                    Id = questionsId,
                                    CategoryId = category.Id,
                                    SubcategoryId = subCategoryId,
                                    Question = question.Question,
                                    Type = question.Type,
                                    QuestionImage = question.Type == 2 ? question.QuestionImage : null,
                                    AnswerImage = question.Type == 2 ? question.AnswerImage : null,
                                    IsMultipleAnswer = question.IsMultipleAnswer
                                });

                                var answersDataModel = (from answers in dbContext.NisQuestionAnswers
                                                        where answers.QuestionId == question.Id
                                                        select new
                                                        {
                                                            answers.Answer,
                                                            answers.Order,
                                                            answers.Marks,
                                                            answers.IsCorrectAnswer
                                                        }).ToList();

                                foreach (var answer in answersDataModel)
                                {
                                    answersList.Add(new NisQuestionAnswers()
                                    {
                                        Id = Guid.NewGuid(),
                                        QuestionId = questionsId,
                                        Answer = answer.Answer,
                                        Order = answer.Order,
                                        Marks = answer.Marks,
                                        IsCorrectAnswer = answer.IsCorrectAnswer
                                    });
                                }
                            }
                        }

                        dbContext.NisQuestions.AddRange(questionsList);
                        dbContext.NisQuestionAnswers.AddRange(answersList);
                        dbContext.NisSubcategory.AddRange(subCategoryList);
                        dbContext.NisCategory.Add(category);
                        dbContext.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception(string.Format(Messages.OT_006, categoryDataModel.Name, "Test Name"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDataModel> GetCategoryList()
        {
            var categotyDataModel = new List<CategoryDataModel>();

            var categoryList = _context.NisCategory.OrderByDescending(x => x.CreatedDate)
                                                  .ThenBy(x => x.IsPublished).Take(5).ToList();
            foreach (var item in categoryList)
            {
                categotyDataModel.Add(new CategoryDataModel()
                {
                    CategoryId = item.Id,
                    IsPublished = item.IsPublished,
                    Name = item.Name,
                    TotalTime = item.Time,
                    TotalCategoryMarks = item.TotalMarks,
                    IsShuffle = item.IsShuffleQuestions
                });
            }
            return categotyDataModel;
        }

        public List<SubCategoryGroupDataModel> GetCloneList()
        {
            var categotyDataModel = new List<SubCategoryGroupDataModel>();

            var categoryLatest = _context.NisCategory.Where(x => x.IsPublished)
                                                     .Select(x => new { x.Id, x.Name, x.CreatedDate })
                                                     .OrderByDescending(x => x.CreatedDate)
                                                     .Take(1).ToList();

            var categoryGroup = (from category in categoryLatest
                                 join subcategory in _context.NisSubcategory on category.Id equals subcategory.CategoryId
                                 select new
                                 {
                                     CategoryName = category.Name,
                                     CategoryId = category.Id,
                                     SubCategoryId = subcategory.Id,
                                     SubCategoryName = subcategory.Name,
                                     subcategory.Marks,
                                     category.CreatedDate
                                 }).OrderBy(x => x.SubCategoryName).ToList();


            foreach (var item in categoryGroup)
            {
                categotyDataModel.Add(new SubCategoryGroupDataModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    SubCategoryId = item.SubCategoryId,
                    SubCategoryName = item.SubCategoryName,
                    IsSelected = false,
                    Marks = item.Marks
                });
            }
            return categotyDataModel;
        }

        public bool UpdateCategory(CategoryDataModel categoryDataModel)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                    var categoryData = dbContext.NisCategory.SingleOrDefault(x => x.Id == categoryDataModel.CategoryId);

                    if (categoryData != null)
                    {
                        if (categoryData.Name.Trim() != categoryDataModel.Name.Trim())
                        {
                            if (!IsCategoryNameExists(categoryDataModel.Name))
                            {
                                categoryData.Name = categoryDataModel.Name.Trim();
                            }
                            else
                            {
                                throw new Exception(string.Format(Messages.OT_006, categoryDataModel.Name, "Test Name"));
                            }
                        }

                        categoryData.Time = categoryDataModel.TotalTime;
                        categoryData.IsShuffleQuestions = categoryDataModel.IsShuffle;
                        categoryData.TotalMarks = categoryDataModel.TotalCategoryMarks;
                        dbContext.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception(Messages.OT_008);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsCategoryNameExists(string categoryName)
        {
            try
            {
                using (var dbContext = new VinayTestDBContext())
                {
                  

                    var categoryNameAlreadyExist = dbContext.NisCategory.FirstOrDefault(x => x.Name == categoryName.Trim());

                    if (categoryNameAlreadyExist == null)
                        return false;
                }

                return true;
            }
            catch (Exception)
            {
                throw new Exception(Messages.OT_0018);
            }
        }
    }
}
