using System;

namespace NichiOnlineTest.API.Data.ViewModels
{
    public class TokenViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
