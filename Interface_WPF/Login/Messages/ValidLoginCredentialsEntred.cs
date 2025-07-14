using Interface_WPF.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Login.Messages
{
    public class ValidLoginCredentialsEntred
    {
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public ValidLoginCredentialsEntred(User user)
        {
            User = user;
        }
    }
}
