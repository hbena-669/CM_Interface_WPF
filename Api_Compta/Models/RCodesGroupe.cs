using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class RCodesGroupe
    {
        public RCodesGroupe()
        {
            RCGCodesCompta = new HashSet<RCGCodesComptum>();
        }

        public Guid CodeGroupeId { get; set; }
        public Guid RubriqueId { get; set; }
        public string CodeGroupe { get; set; } = null!;
        public string Libelle { get; set; } = null!;

        public virtual Rubrique Rubrique { get; set; } = null!;
        public virtual ICollection<RCGCodesComptum> RCGCodesCompta { get; set; }
    }
}
