using System.ComponentModel.DataAnnotations;

namespace NichiOnlineTest.API.Data.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
