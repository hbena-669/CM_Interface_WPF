using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.ViewModels;
using Interface_WPF.Login.Messages;
using Interface_WPF.Login.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF
{
    public class ShellViewModel : Conductor<Screen>,
        IHandle<SuccessFullyAuthentificatedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LogingConductorViewModel _logingConductorViewModel;
        private readonly ContentConductorViewModel _contentConductorViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, 
                                LogingConductorViewModel logingConductorViewModel,
                                ContentConductorViewModel contentConductorViewModel)
        {
            _eventAggregator = eventAggregator;
            _contentConductorViewModel = contentConductorViewModel;
            _logingConductorViewModel = logingConductorViewModel;

            //Items.AddRange( new Screen[] { _logingConductorViewModel, _contentConductorViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_logingConductorViewModel);
            
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(SuccessFullyAuthentificatedMessage message)
        {
            ActivateItem(_contentConductorViewModel);
        }

       
    }

    
}
