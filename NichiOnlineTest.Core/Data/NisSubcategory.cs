using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisSubcategory
    {
        public NisSubcategory()
        {
            NisQuestions = new HashSet<NisQuestions>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? ClonedId { get; set; }
        public decimal Marks { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual NisCategory Category { get; set; }
        public virtual ICollection<NisQuestions> NisQuestions { get; set; }
    }
}
