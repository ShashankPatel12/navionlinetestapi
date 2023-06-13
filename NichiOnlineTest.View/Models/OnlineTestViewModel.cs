using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NichiOnlineTest.View.Models
{
    public class OnlineTestViewModel
    {
        [JsonProperty(PropertyName = "totalQuestions")]
        public int TotalQuestions { get; set; }
        [JsonProperty(PropertyName = "attendedQuestions")]
        public int AttendedQuestions { get; set; }
        [JsonProperty(PropertyName = "time")]
        public double Time { get; set; }
        [JsonProperty(PropertyName = "timeRemaining")]
        public TimeSpan TimeRemaining { get; set; }
        [JsonProperty(PropertyName = "categoryId")]
        public Guid CategoryId { get; set; }
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty(PropertyName = "isTestSubmitted")]
        public bool IsTestSubmitted { get; set; }
        [JsonProperty(PropertyName ="userId")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "subCategories")]
        public List<SubCategory> SubCategories { get; set; }

    }

    public class SubCategory
    {
        [JsonProperty(PropertyName = "subCategoryId")]
        public Guid SubCategoryId { get; set; }
        [JsonProperty(PropertyName = "subCategoryName")]
        public string SubCategoryName { get; set; }
        [JsonProperty(PropertyName = "selectedQuestionIndex")]
        public int SelectedQuestionIndex { get; set; }
        [JsonProperty(PropertyName = "questions")]
        public List<QuestionsViewModel> Questions { get; set; }
        [JsonProperty(PropertyName = "totalQuestions")]
        public int TotalQuestions { get; set; }
        [JsonProperty(PropertyName = "attendedQuestions")]
        public int AttendedQuestions { get; set; }

    }

    public class QuestionsViewModel
    {
        [JsonProperty(PropertyName = "questionId")]
        public Guid QuestionId { get; set; }
        [JsonProperty(PropertyName = "questionType")]
        public int QuestionType { get; set; }
        [JsonProperty(PropertyName = "questionImg")]
        public string QuestionImg { get; set; }
        [JsonProperty(PropertyName = "answerImg")]
        public string AnswerImg { get; set; }
        [JsonProperty(PropertyName = "questionText")]
        public string QuestionText { get; set; }
        [JsonProperty(PropertyName = "isMultiple")]
        public bool IsMultiple { get; set; }
        [JsonProperty(PropertyName = "isAnswered")]
        public bool IsAnswered { get; set; }
        [JsonProperty(PropertyName = "marks")]
        public decimal Marks { get; set; }
        [JsonProperty(PropertyName = "answers")]
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        [JsonProperty(PropertyName = "answerId")]
        public Guid AnswerId { get; set; }
        [JsonProperty(PropertyName = "answerText")]
        public string AnswerText { get; set; }
        [JsonProperty(PropertyName = "isSelected")]
        public bool IsSelected { get; set; }
        [JsonProperty(PropertyName = "userAnswer")]
        public string UserAnswer { get; set; }
        [JsonProperty(PropertyName = "marks")]
        public decimal Marks { get; set; }
    }
}
