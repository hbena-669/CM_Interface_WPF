using Interface_WPF.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Repositories
{
    public interface IOrdersRepository
    {
        public IList<Order> GetOrders();
        public Order GetOrder(int id);
    }
}
