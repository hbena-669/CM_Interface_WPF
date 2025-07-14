using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_WPF.Login.Messages;

namespace Interface_WPF.Login.ViewModels
{
    public class LoginCredentialsViewModel:Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private string  _userName;

        public string  UserName
        {
            get { return _userName; }
            set { 
                _userName = value;
                NotifyOfPropertyChange(() =>UserName);
                NotifyOfPropertyChange(nameof(CanContinue));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(nameof(CanContinue));
            }
        }

        public LoginCredentialsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }
        public void Continue()
        {
            _eventAggregator.PublishOnUIThread(new ValidLoginCredentialsEntred(new Content.Models.User(_userName,_password,string.Empty)));
        }

        public bool CanContinue => !string.IsNullOrWhiteSpace(UserName); //&& !string.IsNullOrWhiteSpace(Password);


    }
}
