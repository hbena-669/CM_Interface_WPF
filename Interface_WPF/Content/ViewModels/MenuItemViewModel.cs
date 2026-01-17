using Caliburn.Micro;
using FontAwesome.WPF;
using Interface_WPF.Content.Messages;
namespace Interface_WPF.Content.ViewModels
{
    public class MenuItemViewModel : PropertyChangedBase
    {
        private bool _isSelected;
        public string Title { get; }
        public ContentPage Page { get; }
        public FontAwesomeIcon Icon { get; }
        public string ToolTip { get; }
        public string Text { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public MenuItemViewModel(string title, ContentPage page, FontAwesomeIcon icon, string toolTip)
        {
            Page = page;
            Icon = icon;
            ToolTip = toolTip;
            Title = title;
        }
    }
}
