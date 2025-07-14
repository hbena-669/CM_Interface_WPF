using Interface_WPF.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_WPF.Content.Messages
{
    public class NavigateMessage
    {
        private ContentPage _page;

        public ContentPage Page
        {
            get { return _page; }
            set { _page = value; }
        }

        

        public NavigateMessage(ContentPage page, User? user = null)
        {
            _page = page;
        }
    }

    public enum ContentPage
    {
        Home,
        Settings,
        Orders
    }
}
