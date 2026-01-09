using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Dtos
{
    public class EtablissementDto
    {
        public Guid EtablissementId { get; set; }
        public string Code { get; set; } = null!;
        public string Nom { get; set; } = null!;

    }
}
