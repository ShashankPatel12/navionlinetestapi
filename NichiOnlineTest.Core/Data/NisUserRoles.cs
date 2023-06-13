using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual NisRoles Role { get; set; }
        public virtual NisUsers User { get; set; }
    }
}
