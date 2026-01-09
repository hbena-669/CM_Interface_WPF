using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class Role
    {
        public Role()
        {
            UserEtablissementRoles = new HashSet<UserEtablissementRole>();
            Permissions = new HashSet<Permission>();
        }

        public Guid RoleId { get; set; }
        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<UserEtablissementRole> UserEtablissementRoles { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
