using System;
using System.Collections.Generic;

namespace Api_Compta.Models
{
    public partial class RCGCodesComptum
    {
        public Guid CodeComptaId { get; set; }
        public Guid CodeGroupeId { get; set; }
        public string CodeCompta { get; set; } = null!;

        public virtual RCodesGroupe CodeGroupe { get; set; } = null!;
    }
}
