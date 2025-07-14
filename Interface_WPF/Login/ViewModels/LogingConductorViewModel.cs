using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.ViewModels;
using Interface_WPF.Login.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Login.ViewModels
{
    public class LogingConductorViewModel: Conductor<Screen>.Collection.OneActive,
        IHandle<ValidLoginCredentialsEntred>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginCredentialsViewModel _loginCredentialsViewModel;
        private readonly Login2FAViewModel _login2FAViewModel;

        public LogingConductorViewModel(IEventAggregator eventAggregator,
                                        LoginCredentialsViewModel loginCredentialsViewModel,
                                        Login2FAViewModel login2FAViewModel)
        {
            _eventAggregator = eventAggregator;
            _loginCredentialsViewModel = loginCredentialsViewModel;
            _login2FAViewModel = login2FAViewModel;

            Items.AddRange(new Screen[] { _loginCredentialsViewModel, _login2FAViewModel });

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_loginCredentialsViewModel);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }
        public void Handle(ValidLoginCredentialsEntred message)
        {
            ActivateItem(_login2FAViewModel);
        }
    }

    
}
