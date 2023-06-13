using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.Core.Models;
using NichiOnlineTest.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NichiOnlineTest.View.Logic
{
    public class QuestionViewLogic
    {
        private QuestionCoreLogic _coreLogic = new QuestionCoreLogic();

        public List<CategoryViewModel> GetAllCategory()
        {
            CategoryCoreLogic logic = new CategoryCoreLogic();
            return logic.GetAllCategory()
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new CategoryViewModel
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    TotalCategoryMarks = x.TotalCategoryMarks,
                    TotalTime = x.TotalTime,
                    IsPublished = x.IsPublished,
                    IsShuffle = x.IsShuffle
                })
                .Take(5).ToList();
        }

        public List<QuestionCategorySummary> GetCategorySummary(Guid categoryId)
        {
            var response = _coreLogic.GetSubCategorySummaryByCategoryId(categoryId)
                                    .Select(x => new QuestionCategorySummary()
                                    {
                                        CategoryId = x.CategoryId,
                                        CategoryName = x.CategoryName,
                                        SubCategoryId = x.SubCategoryId,
                                        SubCategoryName = x.SubCategoryName,
                                        TotalMarks = x.SubCategoryMarks,
                                        CategoryTotalMarks = x.CategoryMarks,
                                        IsCategoryPublished = x.IsCategoryPublished
                                    }).ToList();

            return response;
        }

        public QuestionViewModel GetQuestionsBySubCategoryId(Guid categoryId, Guid subCategoryid)
        {
            var questionDataModel = _coreLogic.GetQuestionsBySubCategoryId(categoryId, subCategoryid);

            var questionList = new List<Question>();

            foreach (var question in questionDataModel.Questions)
            {
                var answersList = question.Answers
                                          .Select(answer => new Answer()
                                          {
                                              Id = answer.Id,
                                              ForQuestionId = answer.ForQuestionId,
                                              Text = answer.Text,
                                              Marks = answer.Marks.ToString(),
                                              IsCorrectAnswer = answer.IsCorrectAnswer,
                                              Order = answer.Order
                                          }).ToList();

                questionList.Add(new Question()
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type,
                    Marks = question.Marks.ToString(),
                    QuestionImageURL = question.QuestionImageURL,
                    AnswerImageURL = question.AnswerImageURL,
                    Answers = answersList,
                    IsMultipleAnswer = question.IsMultipleAnswer
                });
            }

            return new QuestionViewModel()
            {
                CategoryId = questionDataModel.CategoryId,
                CategoryName = questionDataModel.CategoryName,
                SubCategoryId = questionDataModel.SubCategoryId,
                SubCategoryName = questionDataModel.SubCategoryName,
                IsCategoryPublished = questionDataModel.IsCategoryPublished,
                Questions = questionList
            };
        }

        public bool Create(QuestionViewModel viewModel)
        {
            var dataModelList = new List<QuestionDataModel>();

            foreach (var question in viewModel.Questions)
            {
                if (question.IsMultipleAnswer &&
                    question.Answers.Where(x => x.IsCorrectAnswer).Count() == 1)
                {
                    //throw an business logic error where correctanswer should be more than 1
                }

                var dataModel = new QuestionDataModel()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = viewModel.CategoryId,
                    SubCategoryId = viewModel.SubCategoryId,
                    Title = question.Title,
                    Type = question.Type,
                    Marks = Convert.ToInt16(question.Marks),
                    QuestionImageURL = question.Type == 2 ? question.QuestionImageURL : null,
                    AnswerImageURL = question.Type == 2 ? question.AnswerImageURL : null,
                    IsMultipleAnswer = question.IsMultipleAnswer,
                };

                foreach (var answer in question.Answers)
                {
                    dataModel.Answers = new List<AnswerDataModel>();

                    dataModel.Answers.Add(new AnswerDataModel()
                    {
                        Id = Guid.NewGuid(),
                        ForQuestionId = dataModel.Id,
                        IsCorrectAnswer = answer.IsCorrectAnswer,
                        Marks = question.Type == 2 ? Convert.ToInt16(answer.Marks) : 0,
                        Text = answer.Text
                    });
                }

                dataModelList.Add(dataModel);
            }

            //return _coreLogic.Create(dataModelList);
            return true;
        }

        public bool Update(QuestionViewModel viewModel)
        {
            var dataModelList = new List<QuestionDataModel>();

            foreach (var question in viewModel.Questions)
            {
                var dataModel = new QuestionDataModel()
                {
                    Id = Guid.NewGuid(),
                    CategoryId = viewModel.CategoryId,
                    SubCategoryId = viewModel.SubCategoryId,
                    Title = question.Title,
                    Type = question.Type,
                    Marks = Convert.ToDecimal(question.Marks),
                    QuestionImageURL = question.QuestionImageURL,
                    AnswerImageURL = question.AnswerImageURL,
                    IsMultipleAnswer = question.IsMultipleAnswer,
                    Answers = new List<AnswerDataModel>()
                };

                if (question.Type == 2)
                {
                    if (!string.IsNullOrEmpty(question.QuestionImage))
                    {
                        dataModel.QuestionImageURL = question.QuestionImage;
                    }

                    if (!string.IsNullOrEmpty(question.AnswerImage))
                    {
                        dataModel.AnswerImageURL = question.AnswerImage;
                    }
                }

                var isMarksAssigned = false;

                foreach (var answer in question.Answers)
                {
                    decimal marks = 0;

                    //   marks = isMarksAssigned && question.IsMultipleAnswer && answer.IsCorrectAnswer ? 0 : question.Marks;

                    if (question.Type == 1 && answer.IsCorrectAnswer)
                    {
                        marks = Convert.ToDecimal(question.Marks);
                    }

                    if (question.Type == 2)
                    {
                        marks = Convert.ToDecimal(answer.Marks);
                    }

                    if (isMarksAssigned)
                    {
                        marks = 0;
                    }

                    dataModel.Answers.Add(new AnswerDataModel()
                    {
                        Id = Guid.NewGuid(),
                        ForQuestionId = dataModel.Id,
                        IsCorrectAnswer = question.Type == 2 ? true : answer.IsCorrectAnswer,
                        Marks = marks,
                        Text = answer.Text,
                        Order = answer.Order
                    });

                    if (!isMarksAssigned)
                        isMarksAssigned = question.Type == 1 && question.IsMultipleAnswer && answer.IsCorrectAnswer;
                }

                dataModelList.Add(dataModel);
            }

            var questMaterDataModel = new QuestionMasterDataModel()
            {
                CategoryId = viewModel.CategoryId,
                SubCategoryId = viewModel.SubCategoryId,
                Questions = dataModelList
            };

            return _coreLogic.Update(questMaterDataModel);
        }

        public bool Publish(Guid categoryId)
        {
            return _coreLogic.Publish(categoryId);
        }
    }
}
