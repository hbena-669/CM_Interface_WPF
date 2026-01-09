using Interface_WPF.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Login.Messages
{
    public class SuccessFullyAuthentificatedMessage
    {
        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public SuccessFullyAuthentificatedMessage(string? token = null)
        {
            Token = token ?? string.Empty;
        }
    }
}
