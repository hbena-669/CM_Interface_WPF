using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class User
    {
        public User()
        {
            UserEtablissementRoles = new HashSet<UserEtablissementRole>();
        }

        public Guid UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool Is2FAEnabled { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreation { get; set; }

        public virtual ICollection<UserEtablissementRole> UserEtablissementRoles { get; set; }
    }
}
