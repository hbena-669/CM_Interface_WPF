using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.Models;
using Interface_WPF.Content.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Interface_WPF.Content.ViewModels
{
   
    public class OrdersViewModel:Screen,IHandle<NavigateMessage>
    {
        private string _Title = "None";

        public string Title
        {
            get { return _Title; }
            set { 
                _Title = value; 
                NotifyOfPropertyChange(() => Title);
            }
        }

        private Order _selectedOrder;

        public BindableCollection<Order> Orders { get; } = new BindableCollection<Order>();
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                NotifyOfPropertyChange(() => SelectedOrder);
            }
        }

        private IEventAggregator _EventAggregator;
        private readonly IOrdersRepository _ordersRepository;
        public OrdersViewModel(IEventAggregator eventAggregator, IOrdersRepository ordersRepository)
        {
            //Title = "Liste des Commandes";
            _EventAggregator = eventAggregator;
            _ordersRepository = ordersRepository;
            _EventAggregator.Subscribe(this);

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            //_EventAggregator.Subscribe(this);
            var orders = _ordersRepository.GetOrders();
            Orders.AddRange(orders.Select(x => x));
            SelectedOrder = Orders.First();

        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _EventAggregator.Unsubscribe(this);
            Orders.Clear();

        }
        public void Handle(NavigateMessage message)
        {
            Title = "Orders list";
        }
    }
}
