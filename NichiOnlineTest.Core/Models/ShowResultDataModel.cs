using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Models
{
    public class ShowResultDataModel
    {
        public string CandidateName { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalMarks { get; set; }
        public List<ShowResultSubcategoryDataModel> SubcategoryList { get; set; }

    }

    public class ShowResultSubcategoryDataModel
    {
        public Guid SubcategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Marks { get; set; }
        public List<ShowResultQuestionDataModel> Questions { get; set; }

    }

    public class ShowResultQuestionDataModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public string Title { get; set; }

        public decimal Marks { get; set; }

        public int Type { get; set; }

        public string QuestionImageURL { get; set; }

        public string AnswerImageURL { get; set; }

        public bool IsMultipleAnswer { get; set; }
        public List<ShowResultAnswerDataModel> AnswerOptions { get; set; }
    }

    public class ShowResultAnswerDataModel
    {
        public Guid Id { get; set; }

        public Guid ForQuestionId { get; set; }

        public string Text { get; set; }
        public int Order { get; set; }

        public decimal Marks { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public bool IsUserCorrectAnswer { get; set; }
        public Guid ForUserAnswerId { get; set; }
        public string Text2 { get; set; }
    }
}
