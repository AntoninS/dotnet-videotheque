using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using videotheque.Commands;
using videotheque.Model;

namespace videotheque.ViewModel
{
    class AjouterSerieViewModel
    {
        public Media Media { get; set; }

        private ICommand _ajouterSerieCommand;

        private Window currentWindow;

        public ObservableCollection<Media> ListSerie;

        public AjouterSerieViewModel() { }

        public AjouterSerieViewModel(Window window, ObservableCollection<Media> listSerie)
        {
            this.currentWindow = window;
            this.ListSerie = listSerie;
            this.Media = new Media();
        }

        //Ajout du film
        public ICommand AjouterSerieCommand
        {
            get
            {
                return _ajouterSerieCommand ?? (_ajouterSerieCommand = new AjouterSerieCommand(() => ExecuteMethodAjoutSerie(), () => CanExecuteAjoutSerie));
            }
        }

        public bool CanExecuteAjoutSerie
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodAjoutSerie()
        {
            this.AjouterSerieDb();
            this.currentWindow.Close();
        }

        public async void AjouterSerieDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Media.TypeMedia = ETypeMedia.TypeMedia.Serie;
            context.Medias.Add(this.Media);
            this.ListSerie.Add(this.Media);

            context.SaveChanges();

        }

    }
}
