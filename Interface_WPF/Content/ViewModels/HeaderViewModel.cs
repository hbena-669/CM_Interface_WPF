using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface_WPF.Content.Messages;

namespace Interface_WPF.Content.ViewModels
{
    public class HeaderViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.PublishOnUIThread(new NavigateMessage(ContentPage.Home));
        }

        public void Home()
        {
            _eventAggregator.PublishOnUIThread(new NavigateMessage(ContentPage.Home));
        }

        public void Settings()
        {
            _eventAggregator.PublishOnUIThread(new NavigateMessage(ContentPage.Settings));
        }

        public void Orders()
        {
            _eventAggregator.PublishOnUIThread(new NavigateMessage(ContentPage.Orders));
        }
    }
}
