using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace NichiOnlineTest.View.Models
{
    public class SubcategoryViewModel
    {
        [Display(Name= "SubCategoryId")]
        [JsonProperty(PropertyName = "subCategoryId")]
        public Guid? SubCategoryId { get; set; }

       // [Required]
        [Display(Name = "Name")]
        [JsonProperty(PropertyName = "subCategoryName")]
        public string Name { get; set; }

        [Display(Name= "CategoryId")]
        [JsonProperty(PropertyName = "categoryId")]
        public Guid CategoryId { get; set; }

        [Display(Name = "ClonedId")]
        [JsonProperty(PropertyName = "clonedId")]
        public Guid? ClonedId { get; set; }

        [Display(Name = "SubCategoryTotalMarks")]
        [JsonProperty(PropertyName = "subCategoryTotalMarks")]
        public int SubCategoryTotalMarks { get; set; }

        [Display(Name = "CategoryName")]
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName{ get; set; }

        [Display(Name = "IsPublished")]
        [JsonProperty(PropertyName = "isPublished")]
        public bool IsPublished { get; set; }
    }

    public class ResponseViewModel<T>   
    {
        public string MessageCode { get; set; }

        public string[] ErrorMessage { get; set; }

        public T Model { get; set; }
    }
}
