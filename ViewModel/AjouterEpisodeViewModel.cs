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
    class AjouterEpisodeViewModel
    {
        public Episode Episode { get; set; }

        private Window currentWindow;

        public ObservableCollection<Episode> ListEpisode;

        private ICommand _ajouterEpisodeCommand;

        public AjouterEpisodeViewModel() { }

        public AjouterEpisodeViewModel(Window window, ObservableCollection<Episode> listEpisode, int idSerie)
        {
            this.currentWindow = window;
            this.ListEpisode = listEpisode;
            this.Episode = new Episode();
            this.Episode.IdMedia = idSerie;
            Console.WriteLine(this.Episode.IdMedia);
        }

        //Ajout de l'épisode à la série
        public ICommand AjouterEpisodeCommand
        {
            get
            {
                return _ajouterEpisodeCommand ?? (_ajouterEpisodeCommand = new AjouterEpisodeCommand(() => ExecuteMethodAjoutEpisode(), () => CanExecuteAjoutEpisode));
            }
        }

        public bool CanExecuteAjoutEpisode
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodAjoutEpisode()
        {
            this.AjouterEpisodeDb();
            this.currentWindow.Close();
        }

        public async void AjouterEpisodeDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.ListEpisode.Add(this.Episode);

            Console.WriteLine(this.Episode.IdMedia+" " +this.Episode.Titre + " ; " + this.Episode.Duree + " ; " + this.Episode.Description + " ; " + this.Episode.DateDiffusion + " ; ");

            context.Episodes.Add(this.Episode);
            context.SaveChanges();

        }
    }
}
