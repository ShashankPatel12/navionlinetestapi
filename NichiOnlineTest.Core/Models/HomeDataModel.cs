using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Models
{
    public class HomeDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public List<SubcategoryDataModel> SubcategoryDataModels { get; set; }
        public int TotalMarks { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public List<OnlineTestDataModel> UserDataModels { get; set; }
    }
}
