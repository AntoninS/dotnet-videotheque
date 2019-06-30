using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using videotheque.Commands;
using videotheque.Model;

namespace videotheque.ViewModel
{
    class AjouterFilmViewModel
    {

        public Media Media { get; set; }

        private ICommand _ajouterFilmCommand;

        private Window currentWindow;

        public ObservableCollection<Media> ListFilm;

        public AjouterFilmViewModel() { }

        public AjouterFilmViewModel(Window window, ObservableCollection<Media> listFilm)
        {
            this.currentWindow = window;
            this.ListFilm = listFilm;
            this.Media = new Media();
        }

        //Ajout du film
        public ICommand AjouterFilmCommand
        {
            get
            {
                return _ajouterFilmCommand ?? (_ajouterFilmCommand = new AjouterFilmCommand(() => ExecuteMethodAjoutFilm(), () => CanExecuteAjoutFilm));
            }
        }

        public bool CanExecuteAjoutFilm
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodAjoutFilm()
        {
            this.AjouterFilmDb();
            this.currentWindow.Close();
        }

        public async void AjouterFilmDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Media.TypeMedia = ETypeMedia.TypeMedia.Film;
            context.Medias.Add(this.Media);
            this.ListFilm.Add(this.Media);

            context.SaveChanges();

        }
    }

}
