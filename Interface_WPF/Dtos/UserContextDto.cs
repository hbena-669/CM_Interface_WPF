using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Dtos
{
    public class UserContextDto
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }

        public List<EtablissementDto> Etablissements { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}
