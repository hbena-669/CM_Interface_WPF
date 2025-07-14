using Interface_WPF.Content.Models;

namespace Interface_WPF.Content.Repositories
{
    public class BooksRepository: IBooksRepository
    {
        private IList<Book> books = new List<Book>()
            { new Book("Test","Jone Doeuf",1000,EnumCategories.Fiction), 
            new Book("Partir loin"," Le Merleu",102, EnumCategories.Policier),
            new Book("Ici c'est mieux", "La carpe", 993, EnumCategories.Fiction),
            new Book("Test long","Test long trés long Test long trés long Test long trés long Test long trés long",1000, EnumCategories.Autre)
            };
        public BooksRepository() { }
        public IList<Book> GetBooks() 
        {
            
            return books; 
        }

        public void Remove(Book book)
        { 
            books.Remove(book); 
        }
    }
}