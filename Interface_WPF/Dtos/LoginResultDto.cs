using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Dtos
{
    public class LoginResultDto
    {
        public string Token { get; set; } = string.Empty;
        public bool Is2FAEnabled { get; set; }
    }
}
