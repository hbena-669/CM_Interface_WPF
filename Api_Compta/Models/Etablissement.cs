using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class Etablissement
    {
        public Etablissement()
        {
            UserEtablissementRoles = new HashSet<UserEtablissementRole>();
        }

        public Guid EtablissementId { get; set; }
        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime DateCreation { get; set; }

        public virtual ICollection<UserEtablissementRole> UserEtablissementRoles { get; set; }
    }
}
