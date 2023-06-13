using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Models
{
    public class QuestionMasterDataModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Guid SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public bool IsCategoryPublished { get; set; }

        public List<QuestionDataModel> Questions { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }
    }

    public class QuestionDataModel
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

        public List<AnswerDataModel> Answers { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }
    }

    public class AnswerDataModel
    {
        public Guid Id { get; set; }

        public Guid ForQuestionId { get; set; }

        public string Text { get; set; }

        public decimal Marks { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public int Order { get; set; }
    }

    public class CategoryMasterDataModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Guid SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public int TotalMarks { get; set; }
    }

    public class SubCategorySummary
    {
        public Guid CategoryId { get; set; }

        public Guid SubCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public decimal CategoryMarks { get; set; }

        public decimal SubCategoryMarks { get; set; }

        public bool IsCategoryPublished { get; set; }
    }


}
