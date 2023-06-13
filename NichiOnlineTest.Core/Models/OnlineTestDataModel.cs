using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Models
{
    public class OnlineTestDataModel
    {
        public int TotalQuestions { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public string UserId { get; set; }
        public bool IsTestSubmitted { get; set; }
        public int AttendedQuestions { get; set; }

    }

    public class SubCategory
    {
        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int SelectedQuestionIndex { get; set; }
        public List<Questions> Questions { get; set; }
        public int TotalQuestions { get; set; }
        public int AttendedQuestions { get; set; }

    }

    public class Questions
    {
        public Guid QuestionId { get; set; }
        public int QuestionType { get; set; }
        public string QuestionImg { get; set; }
        public string AnswerImg { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsAnswered { get; set; }
        public double Marks { get; set; }
        public List<Answers> Answers { get; set; }
    }

    public class Answers
    {
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsSelected { get; set; }
        public string UserAnswer { get; set; }
        public decimal Marks { get; set; }
    }
}
