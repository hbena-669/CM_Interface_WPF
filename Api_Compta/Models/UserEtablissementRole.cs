using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class UserEtablissementRole
    {
        public Guid UserId { get; set; }
        public Guid EtablissementId { get; set; }
        public Guid RoleId { get; set; }

        public virtual Etablissement Etablissement { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
