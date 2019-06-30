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
using videotheque.Utils;
using videotheque.View;

namespace videotheque.ViewModel
{
    class SerieViewModel : UtilsBinding
    {
        public ObservableCollection<Media> ListSerie { get; set; }

        public ObservableCollection<Media> ListFilm { get; set; }

        public ObservableCollection<Episode> ListEpisode { get; set; }

        private Media serie;

        public Media Serie
        {
            get
            {
                return serie;
            }
            set
            {
                if (SetProperty(ref serie, value))
                {
                    serie = value;
                    if (serie != null)
                    {
                        this.GetEpisodes();
                    }
                };
            }
        }

        private Episode episode;

        public Episode Episode{ get => episode; set => SetProperty(ref episode, value); }
    

        private ICommand _ajouterSerieCommand;

        private ICommand _ajouterEpisodeCommand;

        private ICommand _modifierLeMediaCommand;

        private ICommand _supprimerLeMediaCommand;

        private ICommand _modifierEpisodeCommand;

        private ICommand _supprimerEpisodeCommand;

        public AjouterSerieView ajouterSerieView;

        public AjouterEpisodeView ajouterEpisodeView;

        public ModificationMediaView fenetreModifMedia;

        public ModificationEpisodeView fenetreModifEpisode;

        public SerieViewModel() { }

        public SerieViewModel(ObservableCollection<Media> listSerie, ObservableCollection<Media> listFilm)
        {
            this.ListEpisode = new ObservableCollection<Episode>();
            this.ListSerie = listSerie;
            this.ListFilm = listFilm;

            this.ajouterSerieView = new AjouterSerieView();
            this.ajouterEpisodeView = new AjouterEpisodeView();
            this.fenetreModifMedia = new ModificationMediaView();
            this.fenetreModifEpisode = new ModificationEpisodeView();
        }

        public async void GetEpisodes()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.ListEpisode.Clear();

            foreach (Episode e in context.Episodes.Where(e=> e.IdMedia == this.Serie.Id).ToList())
            {
                this.ListEpisode.Add(e);
            }

        }

        //Ajout d'une série
        public ICommand AjouterSerieCommand
        {
            get
            {
                return _ajouterSerieCommand ?? (_ajouterSerieCommand = new AjouterSerieCommand(() => ExecuteMethodAjouterSerie(), () => CanExecuteAjouterSerie));
            }
        }

        public bool CanExecuteAjouterSerie
        {
            get
            {
                return !this.ajouterSerieView.IsVisible;
            }
        }

        private void ExecuteMethodAjouterSerie()
        {
            this.ajouterSerieView = new AjouterSerieView(this.ListSerie);
            this.ajouterSerieView.Show();
        }

        //Ajout d'un episode à une série
        public ICommand AjouterEpisodeCommand
        {
            get
            {
                return _ajouterEpisodeCommand ?? (_ajouterEpisodeCommand = new AjouterSerieCommand(() => ExecuteMethodAjouterEpisode(), () => CanExecuteAjouterEpisode));
            }
        }

        public bool CanExecuteAjouterEpisode
        {
            get
            {
                if(this.Serie != null && !this.ajouterEpisodeView.IsVisible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void ExecuteMethodAjouterEpisode()
        {
            this.ajouterEpisodeView = new AjouterEpisodeView(this.ListEpisode, this.Serie.Id);
            this.ajouterEpisodeView.Show();
        }

        //Modifier la série choisit par l'utilisateur
        public ICommand ModifierLeMediaCommand
        {
            get
            {
                return _modifierLeMediaCommand ?? (_modifierLeMediaCommand = new ModifierMediaCommand(param => ExecuteMethodModifierMedia(param), () => CanExecuteModifierMedia));
            }
        }

        public bool CanExecuteModifierMedia
        {
            get
            {
                return !this.fenetreModifMedia.IsVisible;
            }
        }

        private void ExecuteMethodModifierMedia(object param)
        {
            this.fenetreModifMedia = new ModificationMediaView(param, this.ListFilm, this.ListSerie);
            this.fenetreModifMedia.Show();
        }

        //Supprimer la série choisis par l'utilisateur
        public ICommand SupprimerLeMediaCommand
        {
            get
            {
                return _supprimerLeMediaCommand ?? (_supprimerLeMediaCommand = new SupprimerMediaCommand(param => ExecuteMethodSupprimerMedia(param), () => CanExecuteSupprimerLeMedia));
            }
        }

        public bool CanExecuteSupprimerLeMedia
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodSupprimerMedia(object param)
        {
            MessageBoxResult result = MessageBox.Show("Voulez vous supprimer cet enregistrement ?", "Attention!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.SuppressionMediaDb((int)param);
                    break;
            }

        }

        public async void SuppressionMediaDb(int id)
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            Media mediaASupprimer = context.Medias.Where(m => m.Id == id).First();

            context.Medias.Remove(mediaASupprimer);

            this.ListSerie.Remove(mediaASupprimer);

            context.SaveChanges();

        }

        //Modifier l'épisode a choisit par l'utilisateur
        public ICommand ModifierEpisodeCommand
        {
            get
            {
                return _modifierEpisodeCommand ?? (_modifierEpisodeCommand = new ModifierMediaCommand(param => ExecuteMethodModifierEpisode(param), () => CanExecuteModifierEpisode));
            }
        }

        public bool CanExecuteModifierEpisode
        {
            get
            {
                return !this.fenetreModifEpisode.IsVisible ;
            }
        }

        private void ExecuteMethodModifierEpisode(object param)
        {
            this.fenetreModifEpisode = new ModificationEpisodeView(this.Serie.Id, (int)param);
            this.fenetreModifEpisode.Show();
        }

        //Supprimer la série choisis par l'utilisateur
        public ICommand SupprimerEpisodeCommand
        {
            get
            {
                return _supprimerEpisodeCommand ?? (_supprimerEpisodeCommand = new SupprimerMediaCommand(param => ExecuteMethodSupprimerEpisode(param), () => CanExecuteSupprimerEpisode));
            }
        }

        public bool CanExecuteSupprimerEpisode
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodSupprimerEpisode(object param)
        {
            MessageBoxResult result = MessageBox.Show("Voulez vous supprimer cet enregistrement ?", "Attention!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.SuppressionEpisodeDb((int)param);
                    break;
            }

        }

        public async void SuppressionEpisodeDb(int id)
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            Episode episodeASupprimer = context.Episodes.Where(e => e.Id == id).Where(e => e.IdMedia == this.Serie.Id).First();

            context.Episodes.Remove(episodeASupprimer);

            this.ListEpisode.Remove(episodeASupprimer);

            context.SaveChanges();

        }

    }
}