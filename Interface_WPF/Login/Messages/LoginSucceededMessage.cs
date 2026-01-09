using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Login.Messages
{
    public class LoginSucceededMessage
    {
        public string Token { get; }
        public bool Is2FAEnabled { get; }

        public LoginSucceededMessage(string token, bool is2FAEnabled)
        {
            Token = token;
            Is2FAEnabled = is2FAEnabled;
        }
    }
}
