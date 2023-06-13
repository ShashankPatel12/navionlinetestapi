using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisQuestionAnswers
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
        public decimal Marks { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual NisQuestions Question { get; set; }
    }
}
