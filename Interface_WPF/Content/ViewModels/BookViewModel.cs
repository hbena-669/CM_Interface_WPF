using Caliburn.Micro;
using Interface_WPF.Content.Models;
using Interface_WPF.Content.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{
    public class BookViewModel:Screen
    {
        private readonly Book _book;
        private readonly IBooksRepository _booksRepository;
        public event EventHandler BookRemoved;

        public BindableCollection<EnumCategories> Categories { get; set; } =
             new BindableCollection<EnumCategories>(
              Enum.GetValues(typeof(EnumCategories)).Cast<EnumCategories>());
        public BookViewModel(Book book, IBooksRepository booksRepository)
        {
            _book = book;
            _booksRepository = booksRepository;
        }

       
        public string Name => _book.Name;
        public string Author => _book.Author;
        public int PageCount =>_book.PageCount;

        public EnumCategories Category
        {
            get => _book.Category;
            set
            {
                if (_book.Category != value)
                {
                    _book.Category = value;
                    NotifyOfPropertyChange(() => Category); // ou NotifyOfPropertyChange(() => Category); si Screen ou PropertyChangedBase
                }
            }
        }
        public void Remove()
        {
            _booksRepository.Remove(_book);
            BookRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
