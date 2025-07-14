using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{

    public class SettingViewModel: Screen,IHandle<NavigateMessage>
    {
        private string _Title = "None";

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
        private IEventAggregator _EventAggregator;
        public SettingViewModel(IEventAggregator eventAggregator)
        {
            _EventAggregator = eventAggregator;
            _EventAggregator.Subscribe(this);
        }
        public void Handle(NavigateMessage message)
        {
            Title = message.Page.ToString();
        }
    }
}
