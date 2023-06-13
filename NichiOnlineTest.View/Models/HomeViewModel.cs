using System;
using System.Collections.Generic;

namespace NichiOnlineTest.View.Models
{
    public class HomeViewModel
    {
        public string CandidateName { get; set; }
        public string MobileNumber { get; set; }
        public List<SubcategoryViewModel> SubcategoryDataModels { get; set; }
        public int TotalMarks { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public List<OnlineTestViewModel> UserDataModels { get; set; }
    }
}
