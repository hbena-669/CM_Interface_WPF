using Caliburn.Micro;
using Interface_WPF.Interfaces;
using Interface_WPF.Login.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Login.ViewModels
{
    public class LoginCredentialsViewModel:Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAuthApi _authApi;
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

        public LoginCredentialsViewModel(IEventAggregator eventAggregator, IAuthApi authApi)
        {
            _eventAggregator = eventAggregator;
            _authApi = authApi;
            
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
        public async void Continue()
        {
            var result = await _authApi.LoginAsync(UserName, Password);

            _eventAggregator.PublishOnUIThread(
                new LoginSucceededMessage(result.Token, result.Is2FAEnabled));
        }

        public bool CanContinue => !string.IsNullOrWhiteSpace(UserName); //&& !string.IsNullOrWhiteSpace(Password);


    }
}
