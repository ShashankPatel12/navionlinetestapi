using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NichiOnlineTest.View.Models
{
    public class CategoryViewModel
    {
        /// <summary>
        /// CategoryId
        /// </summary>
        [JsonProperty(PropertyName = "categoryid")]
        public Guid? CategoryId { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// TotalTime
        /// </summary>
        [JsonProperty(PropertyName = "totaltime")]
        public TimeSpan TotalTime { get; set; }
        /// <summary>
        /// FormattedTime
        /// </summary>
        [JsonProperty(PropertyName = "formattedTime")]
        public string FormattedTime
        {
            get
            {
                return String.Format("{0:hh\\:mm}", TotalTime);
            }
        }
        /// <summary>
        /// TotalCategoryMarks
        /// </summary>
        [JsonProperty(PropertyName = "totalcategorymarks")]
        public decimal TotalCategoryMarks { get; set; }
        /// <summary>
        /// IsShuffle
        /// </summary>
        [JsonProperty(PropertyName = "isshuffle")]
        public bool IsShuffle { get; set; }
        /// <summary>
        /// IsPublished
        /// </summary>
        [JsonProperty(PropertyName = "ispublished")]
        public bool IsPublished { get; set; }
        /// <summary>
        /// IsPublishedString
        /// </summary>
        [JsonProperty(PropertyName = "ispublishedstring")]
        public string IsPublishedString { get; set; }
        /// <summary>
        /// IsClonePreviousCategory
        /// </summary>
        [JsonProperty(PropertyName = "isclonepreviouscategory")]
        public bool IsClonePreviousCategory { get; set; }
        /// <summary>
        /// SubCategoryGroup
        /// </summary>
        [JsonProperty(PropertyName = "subCategoryGroup")]
        public List<SubCategoryGroup> SubCategoryGroup { get; set; }
    }

    public class SubCategoryGroup
    {
        [JsonProperty(PropertyName = "categoryid")]
        public Guid? CategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryname")]
        public string CategoryName { get; set; }

        public Guid? SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        [JsonProperty(PropertyName = "isSelected")]
        public bool IsSelected { get; set; }
        public decimal Marks { get; set; }
    }
}
