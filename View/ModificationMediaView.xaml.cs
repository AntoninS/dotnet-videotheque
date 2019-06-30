using System.Collections.ObjectModel;
using System.Windows;
using videotheque.Model;
using videotheque.ViewModel;

namespace videotheque.View
{
    /// <summary>
    /// Logique d'interaction pour ModificationMediaView.xaml
    /// </summary>
    public partial class ModificationMediaView : Window
    {
        public ModificationMediaView(){}

        public ModificationMediaView(object param, ObservableCollection<Media> listFilm, ObservableCollection<Media> listSerie)
        {
            InitializeComponent();
            DataContext = new ModificationMediaViewModel((int)param, this, listFilm, listSerie);
        }
    }
}
