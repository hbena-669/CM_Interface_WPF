using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Models
{
    public class User
    {
		private string userName;

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

        private string userPassWord;

        public string UserPassWord
        {
            get { return userPassWord; }
            set { userPassWord = value; }
        }

        private string code2FA;

        public string Code2FA
        {
            get { return code2FA; }
            set { code2FA = value; }
        }

        public User()
        {
            
        }
        public User(string userName,  string userPassWord,  string code2FA)
        {
            UserName = userName;
            UserPassWord = userPassWord;
            Code2FA = code2FA;
            
        }
    }
}
