using System;
using System.Collections.Generic;
using System.Text;

namespace NichiOnlineTest.Core.Models
{
    public class ResetUserDataModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name Concat of(First Name and Last Name)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// MobileNumber (as per DB Username)
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
