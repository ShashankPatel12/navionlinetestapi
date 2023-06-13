using System;
using System.Collections.Generic;

namespace NichiOnlineTest.View.Models
{
    public class QuestionViewModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Guid SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public bool IsCategoryPublished { get; set; }

        public List<Question> Questions { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }
    }

    public class Question
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Marks { get; set; }

        public int Type { get; set; }

        public string QuestionImageURL { get; set; }

        public string AnswerImageURL { get; set; }

        public string QuestionImage { get; set; }

        public string AnswerImage { get; set; }

        public bool IsMultipleAnswer { get; set; }

        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public Guid Id { get; set; }

        public Guid ForQuestionId { get; set; }

        public string Text { get; set; }

        public string Marks { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public int Order { get; set; }
    }

    public class QuestionCategorySummary
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Guid SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public decimal TotalMarks { get; set; }

        public decimal CategoryTotalMarks { get; set; }

        public bool IsCategoryPublished { get; set; }
    }
}
