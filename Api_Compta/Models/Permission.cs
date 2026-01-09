using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public Guid PermissionId { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }
    }
}
