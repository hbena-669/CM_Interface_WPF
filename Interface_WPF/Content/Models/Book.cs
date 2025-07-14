using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Models
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public EnumCategories Category { get; set; }
        public Book()
        {
            
        }
        public Book(string name, string author, int pageCount, EnumCategories cat)
        {
            Name = name;
            Author = author;
            PageCount = pageCount;
            Category = cat;
        }
    }

    public enum EnumCategories
    {
        All,
        Fiction,
        Policier,
        Histoire,
        Autre
    }
}
