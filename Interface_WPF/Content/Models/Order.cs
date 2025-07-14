using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public List<Product> Products { get; set; }

        public decimal Total => Products.Sum(p => p.UnitPrice * p.Quantity);
    }
}
