using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.Models;
using Interface_WPF.Content.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{


    public class HomeViewModel:Screen,IHandle<NavigateMessage>
    {
        private string _title;
        public string Title 
        {
            get { return _title; }
            set {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
        private IEventAggregator _EventAggregator;
        private readonly IBooksRepository _booksRepository;
        public BindableCollection<BookViewModel> Books { get; } = new BindableCollection<BookViewModel>();
        private BindableCollection<BookViewModel> _filteredBooks;
        public BindableCollection<BookViewModel> FilteredBooks
        {
            get => _filteredBooks;
            private set
            {
                _filteredBooks = value;
                NotifyOfPropertyChange(() => FilteredBooks);
            }
        }
        public BindableCollection<EnumCategories> Categories { get; set; }
        
        private EnumCategories? _selectedCategory;

        public EnumCategories? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
                FilterBooks();
            }
        }
        private void FilterBooks()
        {
            if (_selectedCategory == null || _selectedCategory == EnumCategories.All)
            {
                FilteredBooks = new BindableCollection<BookViewModel>(Books);
            }
            else
            {
                var filtered = Books
                    .Where(b => b.Category == _selectedCategory)
                    .ToList();
                FilteredBooks = new BindableCollection<BookViewModel>(filtered);
            }
        }

        public HomeViewModel(IBooksRepository  booksRepository, IEventAggregator eventAggregator)
        {
            _booksRepository = booksRepository;
            _EventAggregator = eventAggregator;
            _EventAggregator.Subscribe(this);   

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            var books = _booksRepository.GetBooks();
            Categories = new BindableCollection<EnumCategories>(
                Enum.GetValues(typeof(EnumCategories)).Cast<EnumCategories>()
            );
            Books.AddRange(books.Select(x => CreateBook(x)));
            FilterBooks();  
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            foreach (var book in Books)
            {
                book.BookRemoved -= OnBookRemoved;
            }
            Books.Clear();
        }

        private BookViewModel CreateBook(Book book)
        {
            var bookViewModel = new BookViewModel(book, _booksRepository);
            bookViewModel.BookRemoved += OnBookRemoved;
            return bookViewModel;
        }

        private void OnBookRemoved(object? sender, EventArgs e)
        {
            var bookToRemove = (BookViewModel)sender;
            bookToRemove.BookRemoved -= OnBookRemoved;
            Books.Remove(bookToRemove);
            FilterBooks();
        }

        public void Handle(NavigateMessage message)
        {
            Title = message.Page.ToString();
        }
    }
}
