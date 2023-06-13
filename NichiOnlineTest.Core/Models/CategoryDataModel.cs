using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Models
{
    public class CategoryDataModel
    {
        /// <summary>
        /// CategoryId
        /// </summary>
        public Guid? CategoryId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// TotalTime
        /// </summary>
        public TimeSpan TotalTime { get; set; }
        /// <summary>
        /// TotalCategoryMarks
        /// </summary>
        public decimal TotalCategoryMarks { get; set; }
        /// <summary>
        /// IsShuffle
        /// </summary>
        public bool IsShuffle { get; set; }
        /// <summary>
        /// IsPublished
        /// </summary>
        public bool IsPublished { get; set; }
        /// <summary>
        /// SubCategoryId
        /// </summary>
        public Guid? SubCategoryId { get; set; }
        /// <summary>
        /// SubCategoryName
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IsSelected
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// SubCategoryGroup
        /// </summary>
        public List<SubCategoryGroupDataModel> SubCategoryGroup { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }

    public class SubCategoryGroupDataModel
    {
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsSelected { get; set; }
        public decimal Marks { get; set; }
    }
}
