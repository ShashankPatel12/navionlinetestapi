using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisQuestions
    {
        public NisQuestions()
        {
            NisQuestionAnswers = new HashSet<NisQuestionAnswers>();
            NisUserAnswers = new HashSet<NisUserAnswers>();
        }

        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public string Question { get; set; }
        public int Type { get; set; }
        public string QuestionImage { get; set; }
        public string AnswerImage { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual NisCategory Category { get; set; }
        public virtual NisSubcategory Subcategory { get; set; }
        public virtual ICollection<NisQuestionAnswers> NisQuestionAnswers { get; set; }
        public virtual ICollection<NisUserAnswers> NisUserAnswers { get; set; }
    }
}
