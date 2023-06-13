using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisCategory
    {
        public NisCategory()
        {
            NisQuestions = new HashSet<NisQuestions>();
            NisSubcategory = new HashSet<NisSubcategory>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public decimal TotalMarks { get; set; }
        public bool IsPublished { get; set; }
        public bool IsShuffleQuestions { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual ICollection<NisQuestions> NisQuestions { get; set; }
        public virtual ICollection<NisSubcategory> NisSubcategory { get; set; }
    }
}
