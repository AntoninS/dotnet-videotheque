using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public AjouterFilmViewModel() { }

        public AjouterFilmViewModel(Window window)
        {
            this.currentWindow = window;
            this.Media = new Media();
        }

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

            context.SaveChanges();

        }
    }

}
