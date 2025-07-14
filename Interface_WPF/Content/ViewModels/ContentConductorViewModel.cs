using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Login.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{
    public class ContentConductorViewModel : Conductor<Screen>,
        IHandle<NavigateMessage>
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly HomeViewModel _homeViewModel;
        private readonly SettingViewModel _settingViewModel;
        private readonly OrdersViewModel _ordersViewModel;

        public HeaderViewModel Header {  get; }
        public ContentHeaderViewModel ContentHeader { get; }
        public ContentConductorViewModel(
            IEventAggregator eventAggregator,
            HomeViewModel homeViewModel,
            SettingViewModel settingViewModel,
            HeaderViewModel headerViewModel,
            OrdersViewModel ordersViewModel,
            ContentHeaderViewModel contentHeader)
        {
            _eventAggregator = eventAggregator;
            _homeViewModel = homeViewModel;
            _settingViewModel = settingViewModel;
            Header = headerViewModel;
            _ordersViewModel = ordersViewModel;
            ContentHeader = contentHeader;

            //Items.AddRange(new Screen[] { _homeViewModel, _settingViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            ActivateItem(_homeViewModel);
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _eventAggregator.Unsubscribe(this);
        }
        public void Handle(NavigateMessage message)
        {
            switch (message.Page)
            {
                case ContentPage.Home:
                    ActivateItem(_homeViewModel);
                    break;
                case ContentPage.Settings:
                    ActivateItem(_settingViewModel);
                    break;
                case ContentPage.Orders:
                    ActivateItem(_ordersViewModel);
                    break;
            }
            
        }
    }
}
