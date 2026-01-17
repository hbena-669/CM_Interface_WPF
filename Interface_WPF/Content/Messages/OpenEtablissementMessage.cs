using Interface_WPF.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Messages
{
    public class OpenEtablissementMessage
    {
        public EtablissementDto Etablissement { get; }

        public OpenEtablissementMessage(EtablissementDto etablissement)
        {
            Etablissement = etablissement;
        }
    }
}
