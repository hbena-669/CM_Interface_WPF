using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class Rubrique
    {
        public Rubrique()
        {
            RCodesGroupes = new HashSet<RCodesGroupe>();
        }

        public Guid RubriqueId { get; set; }
        public Guid EtablissementId { get; set; }
        public string Libelle { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int? RBOrder { get; set; }

        public virtual Etablissement Etablissement { get; set; } = null!;
        public virtual ICollection<RCodesGroupe> RCodesGroupes { get; set; }
    }
}
