using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Interfaces;
using Interface_WPF.Login.Messages;
using Interface_WPF.Login.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{
    public class ContentConductorViewModel : Conductor<Screen>,
        IHandle<NavigateMessage>//,IHandle<LoginSucceededMessage>
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly HomeViewModel _homeViewModel;
        private readonly SettingViewModel _settingViewModel;
        private readonly OrdersViewModel _ordersViewModel;
        private readonly IContextService _contextService;
        public HeaderViewModel Header {  get; }
        public HomeComptaViewModel homeComptaViewModel { get; }
        public ContentHeaderViewModel ContentHeader { get; }
        public ContentConductorViewModel(
            IEventAggregator eventAggregator,
            IContextService contextService,
            HomeViewModel homeViewModel,
            SettingViewModel settingViewModel,
            HeaderViewModel headerViewModel,
            OrdersViewModel ordersViewModel,
            ContentHeaderViewModel contentHeader,
            HomeComptaViewModel homeComptaViewModel)
        {
            _eventAggregator = eventAggregator;
            _contextService = contextService;
            _homeViewModel = homeViewModel;
            _settingViewModel = settingViewModel;
            Header = headerViewModel;
            _ordersViewModel = ordersViewModel;
            ContentHeader = contentHeader;
            this.homeComptaViewModel = homeComptaViewModel;

            //Items.AddRange(new Screen[] { _homeViewModel, _settingViewModel });
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _eventAggregator.Subscribe(this);
            //ActivateItem(_homeViewModel);
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
                case ContentPage.HOME:
                    ActivateItem(homeComptaViewModel);
                    break;
                case ContentPage.MENU_ETABLISSEMENTS:
                    ActivateItem(_settingViewModel);
                    break;
                case ContentPage.MENU_BALANCES:
                    ActivateItem(_ordersViewModel);
                    break;
            }
            
        }

        public async void InitializeAfterLogin(LoginSucceededMessage message)
        {
            await _contextService.LoadAsync(message.Token);

            ContentHeader.Initialize(_contextService.Context);
            Header.Initialize(_contextService.Context);
            homeComptaViewModel.Initialize(_contextService.Context);

            ActivateItem(homeComptaViewModel);
        }

        
    }
}
