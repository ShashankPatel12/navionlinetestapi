using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisUsers
    {
        public NisUsers()
        {
            NisUserAnswers = new HashSet<NisUserAnswers>();
            NisUserClaims = new HashSet<NisUserClaims>();
            NisUserLogins = new HashSet<NisUserLogins>();
            NisUserRoles = new HashSet<NisUserRoles>();
            NisUserTestActivity = new HashSet<NisUserTestActivity>();
            NisUsertokens = new HashSet<NisUsertokens>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<NisUserAnswers> NisUserAnswers { get; set; }
        public virtual ICollection<NisUserClaims> NisUserClaims { get; set; }
        public virtual ICollection<NisUserLogins> NisUserLogins { get; set; }
        public virtual ICollection<NisUserRoles> NisUserRoles { get; set; }
        public virtual ICollection<NisUserTestActivity> NisUserTestActivity { get; set; }
        public virtual ICollection<NisUsertokens> NisUsertokens { get; set; }
    }
}
