using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisUserAnswers
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string Question_Answers { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual NisQuestions Question { get; set; }
        public virtual NisUsers User { get; set; }
    }
}
