using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class Balance
    {
        public string AuditNumber { get; set; } = null!;
        public string NumCompte { get; set; } = null!;
        public Guid EtablissementId { get; set; }
        public DateTime DateCompte { get; set; }
        public decimal MvtDebit { get; set; }
        public decimal MvtCredit { get; set; }
        public decimal SoldeDebit { get; set; }
        public decimal SoldeCredit { get; set; }
        public string CodeMapping { get; set; } = null!;
        public string Libelle { get; set; } = null!;
        public byte Status { get; set; }
        public bool? FlagExp { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Etablissement Etablissement { get; set; } = null!;
    }
}
