using NichiOnlineTest.Core.Logic;
using NichiOnlineTest.View.Models;
using System;
using System.Linq;

namespace NichiOnlineTest.View.Logic
{
    public class ShowResultViewLogic
    {
        public ShowResultViewModel GetResultByUser(Guid categoryId, string userName)
        {
            var logic = new ShowResultCoreLogic();
            var result = logic.GetResultByUser(categoryId, userName);

            var showResultViewModel = new ShowResultViewModel
            {
                CandidateName = result.CandidateName,
                CategoryName = result.CategoryName,
                TotalMarks = result.TotalMarks,
                SubcategoryList = result.SubcategoryList.Select(x => new ShowResultSubcategoryViewModel
                {
                    SubCategoryName = x.SubCategoryName,
                    Marks = x.Marks,
                    SubcategoryId = x.SubcategoryId,
                    Questions = x.Questions.Select(c => new ShowResultQuestionViewModel
                    {
                        CategoryId = c.CategoryId,
                        Id = c.Id,
                        Marks = c.Marks,
                        SubCategoryId = c.SubCategoryId,
                        IsMultipleAnswer = c.IsMultipleAnswer,
                        Type = c.Type,
                        AnswerImageURL = c.AnswerImageURL,
                        Title = c.Title,
                        QuestionImageURL = c.QuestionImageURL,
                        AnswerOptions = c.AnswerOptions.Select(a => new ShowResultAnswerViewModel
                        {
                            Id = a.Id,
                            Marks = a.Marks,
                            IsCorrectAnswer = a.IsCorrectAnswer,
                            Text = a.Text,
                            ForQuestionId = a.ForQuestionId,
                            IsUserCorrectAnswer = a.IsUserCorrectAnswer,
                            Text2 = a.Text2,
                            Order = a.Order,
                            ForUserAnswerId = a.ForUserAnswerId
                        }).ToList()

                    }).ToList()
                }).ToList()
            };

            return showResultViewModel;
        }
    }
}
