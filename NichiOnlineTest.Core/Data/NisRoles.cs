using System;
using System.Collections.Generic;

namespace NichiOnlineTest.Core.Data
{
    public partial class NisRoles
    {
        public NisRoles()
        {
            NisRoleClaims = new HashSet<NisRoleClaims>();
            NisUserRoles = new HashSet<NisUserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<NisRoleClaims> NisRoleClaims { get; set; }
        public virtual ICollection<NisUserRoles> NisUserRoles { get; set; }
    }
}
