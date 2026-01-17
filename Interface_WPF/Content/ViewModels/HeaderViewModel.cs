using Caliburn.Micro;
using FontAwesome.WPF;
using Interface_WPF.Content.Messages;
using Interface_WPF.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{
    public class HeaderViewModel : Screen, IHandle<NavigateMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private MenuItemViewModel _selectedItem;

        public BindableCollection<object> Menu { get; } = new();

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this); // S'abonner aux messages
            _eventAggregator.PublishOnUIThread(new NavigateMessage(ContentPage.HOME));
        }

        public void Initialize(UserContextDto context)
        {
            Menu.Clear();

            // Menus simples
            Menu.Add(new MenuItemViewModel(
                "Home",
                ContentPage.HOME,
                FontAwesomeIcon.Home,
                "Accueil"));

            // Groupe Administration
            if (context.HasPermission("ADMIN_ACCESS"))
            {
                var admin = new MenuGroupViewModel(
                    "Administration",
                    FontAwesomeIcon.Cogs);

                if (context.HasPermission("MENU_ETABLISSEMENTS"))
                    admin.Items.Add(new MenuItemViewModel(
                        "Établissements",
                        ContentPage.MENU_ETABLISSEMENTS,
                        FontAwesomeIcon.Building,
                        "Établissements"));

                if (context.HasPermission("MENU_USERS"))
                    admin.Items.Add(new MenuItemViewModel(
                        "Utilisateurs",
                        ContentPage.MENU_USERS,
                        FontAwesomeIcon.Users,
                        "Utilisateurs"));

                if (context.HasPermission("MENU_MENUS"))
                    admin.Items.Add(new MenuItemViewModel(
                        "Menus",
                        ContentPage.MENU_MENUS,
                        FontAwesomeIcon.List,
                        "Menus"));

                if (admin.Items.Any())
                    Menu.Add(admin);
            }

            if (context.HasPermission("MENU_STATS"))
            {
                Menu.Add(new MenuItemViewModel(
                    "Statistiques",
                    ContentPage.MENU_STATS,
                    FontAwesomeIcon.BarChart,
                    "Statistiques & Kpis"));
            }

            if (context.HasPermission("MENU_BALANCES"))
            {
                Menu.Add(new MenuItemViewModel(
                    "Balances",
                    ContentPage.MENU_BALANCES,
                    FontAwesomeIcon.BalanceScale,
                    "Balances"));
            }
        }

        public void Navigate(MenuItemViewModel item)
        {
            // Désélectionner l'ancien item
            if (_selectedItem != null)
                _selectedItem.IsSelected = false;

            // Sélectionner le nouvel item
            item.IsSelected = true;
            _selectedItem = item;

            // Publier la navigation
            _eventAggregator.PublishOnUIThread(
                new NavigateMessage(item.Page));
        }

        // Méthode pour gérer la sélection initiale ou externe
        public void Handle(NavigateMessage message)
        {
            // Trouver l'item correspondant à la page
            var item = FindMenuItemByPage(message.Page);

            if (item != null && item != _selectedItem)
            {
                if (_selectedItem != null)
                    _selectedItem.IsSelected = false;

                item.IsSelected = true;
                _selectedItem = item;
            }
        }

        private MenuItemViewModel FindMenuItemByPage(ContentPage page)
        {
            foreach (var menuObj in Menu)
            {
                // Item simple
                if (menuObj is MenuItemViewModel item && item.Page == page)
                    return item;

                // Groupe
                if (menuObj is MenuGroupViewModel group)
                {
                    var found = group.Items.FirstOrDefault(i => i.Page == page);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }
    }
}
