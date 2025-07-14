using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.Models;
using Interface_WPF.Login.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Interface_WPF.Content.ViewModels
{
    public class ContentHeaderViewModel:Screen,IHandle<SuccessFullyAuthentificatedMessage>
    {
        private User connectedUser;
        public User ConnectedUser
        {
            get { return connectedUser; }
            set { connectedUser = value; }
        }

        private IEventAggregator _EventAggregator;
        public ContentHeaderViewModel(IEventAggregator eventAggregator)
        {
            _EventAggregator = eventAggregator;
            _EventAggregator.Subscribe(this);
        }
        public void Handle(SuccessFullyAuthentificatedMessage message)
        {
            ConnectedUser = message.User;
        }

        public string UserName => "Welcom " + ConnectedUser.UserName;
    }
}
