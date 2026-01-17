using Caliburn.Micro;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.ViewModels
{
    public class MenuGroupViewModel : PropertyChangedBase
    {
        public string Title { get; }
        public FontAwesomeIcon Icon { get; }
        public bool IsExpanded { get; set; }

        public BindableCollection<MenuItemViewModel> Items { get; }

        public MenuGroupViewModel(string title, FontAwesomeIcon icon)
        {
            Title = title;
            Icon = icon;
            Items = new BindableCollection<MenuItemViewModel>();
        }
    }

}
