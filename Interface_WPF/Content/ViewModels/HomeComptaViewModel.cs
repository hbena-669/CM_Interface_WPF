using Caliburn.Micro;
using Interface_WPF.Content.Messages;
using Interface_WPF.Dtos;

namespace Interface_WPF.Content.ViewModels
{
    public class HomeComptaViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public BindableCollection<EtablissementDto> Etablissements { get; }
            = new();

        public HomeComptaViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

        }

        /// <summary>
        /// Appelée UNE SEULE FOIS après login
        /// </summary>
        public void Initialize(UserContextDto context)
        {
            Etablissements.Clear();

            foreach (var etab in context.Etablissements)
                Etablissements.Add(etab);
        }

        public void Open(EtablissementDto etablissement)
        {
            _eventAggregator.PublishOnUIThread(
                new OpenEtablissementMessage(etablissement));
        }
    }
}
