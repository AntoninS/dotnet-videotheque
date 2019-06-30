using System.Collections.ObjectModel;
using System.Windows;
using videotheque.Model;
using videotheque.ViewModel;

namespace videotheque.View
{
    /// <summary>
    /// Logique d'interaction pour SerieView.xaml
    /// </summary>
    public partial class SerieView : Window
    {
        public SerieView() { }

        public SerieView(ObservableCollection<Media> listSerie, ObservableCollection<Media> listFilm)
        {
            InitializeComponent();
            DataContext = new SerieViewModel(listSerie, listFilm);
        }
    }
}
