using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Content.Models;
using Interface_WPF.Dtos;
using Interface_WPF.Login.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Interface_WPF.Content.ViewModels
{
    public class ContentHeaderViewModel:Screen//,IHandle<SuccessFullyAuthentificatedMessage>
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(UserName));
            }
        }

        private string _etablissement;
        public string Etablissement
        {
            get => _etablissement;
            set
            {
                _etablissement = value;
                NotifyOfPropertyChange();
            }
        }

        public string UserName => Login;

        public void Initialize(UserContextDto context)
        {
            Login = context.Login;
            Etablissement = context.Etablissements.FirstOrDefault()?.Nom;
        }

        private IEventAggregator _EventAggregator;
        public ContentHeaderViewModel(IEventAggregator eventAggregator)
        {
            _EventAggregator = eventAggregator;
            _EventAggregator.Subscribe(this);
        }
        //public void Handle(SuccessFullyAuthentificatedMessage message)
        //{
        //    Token = message.Token;
        //}

        //public string UserName => "Welcom " + Login;
    }
}
