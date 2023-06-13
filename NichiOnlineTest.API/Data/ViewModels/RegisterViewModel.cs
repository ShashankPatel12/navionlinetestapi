using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NichiOnlineTest.API.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "MobileNumber")]
        [JsonProperty(PropertyName ="MobileNo")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        //[EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

