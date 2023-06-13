using System;

namespace NichiOnlineTest.Core.Models
{
    public class SubcategoryDataModel
    {
        public Guid? SubCategoryId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? ClonedId { get; set; }
        public int SubCategoryTotalMarks { get; set; }
        public string CategoryName { get; set; }
        public bool IsPublished { get; set; }
    }
}
