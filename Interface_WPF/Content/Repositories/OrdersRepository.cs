using Caliburn.Micro;
using Interface_WPF.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Interface_WPF.Content.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetOrders()
        {
            var Orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Status = "Validée",
                    ClientFirstName = "Alice",
                    ClientLastName = "Dupont",
                    Products = new List<Product>
                    {
                        new Product { Name = "Produit A", Quantity = 2, UnitPrice = 10 },
                        new Product { Name = "Produit B", Quantity = 1, UnitPrice = 20 },
                    }
                },
                new Order
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(-1),
                    Status = "En Attente",
                    ClientFirstName = "Pierre",
                    ClientLastName = "Martin",
                    Products = new List<Product>
                    {
                        new Product { Name = "Produit A", Quantity = 2, UnitPrice = 10 },
                        new Product { Name = "Produit C", Quantity = 2, UnitPrice = 15 },
                    }
                },
                new Order
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(-1),
                    Status = "Validée",
                    ClientFirstName = "Jack",
                    ClientLastName = "Daniel",
                    Products = new List<Product>
                    {
                        new Product { Name = "Produit B", Quantity = 2, UnitPrice = 5 },
                        new Product { Name = "Produit C", Quantity = 2, UnitPrice = 15 },
                    }
                },
                 new Order
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(-2),
                    Status = "Supprimée",
                    ClientFirstName = "Jon",
                    ClientLastName = "Doeuf",
                    Products = new List<Product>
                    {
                        new Product { Name = "Produit D", Quantity = 2, UnitPrice = 3 },
                        new Product { Name = "Produit C", Quantity = 2, UnitPrice = 15 },
                    }
                }
            };
            return Orders;
        }
    }
}
