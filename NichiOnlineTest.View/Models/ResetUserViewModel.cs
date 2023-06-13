using Newtonsoft.Json;

namespace NichiOnlineTest.View.Models
{
    /// <summary>
    /// ResetUserViewModel
    /// </summary>
    public class ResetUserViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name Concat of(First Name and Last Name)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        /// <summary>
        /// MobileNumber (as per DB Username)
        /// </summary>
        [JsonProperty(PropertyName = "mobilenumber")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
