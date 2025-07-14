using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_WPF.Login.Messages;
using Interface_WPF.Content.Models;

namespace Interface_WPF.Login.ViewModels
{
    public class Login2FAViewModel:Screen, 
        IHandle<ValidLoginCredentialsEntred>
    {
        private readonly IEventAggregator _eventAggregator;

        private string  code2FA;

        public string Code2FA
        {
            get { return code2FA; }
            set {
                code2FA = value;
                NotifyOfPropertyChange(() => Code2FA);
                NotifyOfPropertyChange(nameof(CanContinue)); // <-- nécessaire pour mettre à jour l’état du bouton
            }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyOfPropertyChange(() => _user);
            }
        }
        public Login2FAViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
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
            _eventAggregator.PublishOnUIThread(new SuccessFullyAuthentificatedMessage(new Content.Models.User(User.UserName,User.UserPassWord,Code2FA)));
        }

        public void Handle(ValidLoginCredentialsEntred message)
        {
            User = message.User;
            //User.Code2FA = code2FA;
            //UserName = message.User.UserName;
        }

        public bool CanContinue => !string.IsNullOrWhiteSpace(Code2FA);
    }
}
