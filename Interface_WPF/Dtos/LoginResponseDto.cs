using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Dtos
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public LoginResultDto? Result { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
