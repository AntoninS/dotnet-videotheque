using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using videotheque.Commands;
using videotheque.Model;
using videotheque.Utils;
using videotheque.View;

namespace videotheque.ViewModel
{
    class MainWindowViewModel : UtilsBinding
    {

        public ObservableCollection<Media> ListFilm { get; set; }
        public ObservableCollection<Media> ListSerie { get; set; }

        public int nbMedia { get; set; }

        public int nbFilms { get; set; }

        public int nbSeries { get; set; }

        private ICommand _modifierLeMediaCommand;

        private ICommand _supprimerLeMedia;

        private ICommand _ouvrirFenetreFilm;

        private ICommand _ouvrirFenetreSerie;

        public ModificationMediaView fenetreModifMedia;

        public FilmView fenetreFilm;

        public SerieView fenetreSerie;

        public MainWindowViewModel()
        { 

            this.ListFilm = new ObservableCollection<Media>();

            this.ListSerie = new ObservableCollection<Media>();

            this.LoadData();

            this.nbMedia = this.ListFilm.Count() + this.ListSerie.Count();

            this.nbFilms = this.ListFilm.Count();

            this.nbSeries = this.ListSerie.Count();

            this.fenetreModifMedia = new ModificationMediaView();

            this.fenetreFilm = new FilmView();

            this.fenetreSerie = new SerieView();

        }

        //Chargement de tous les médias et séparation film/série
        public async void LoadData()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            foreach(Media f in context.Medias.Where(m => m.TypeMedia.ToString() == "Film").ToList())
            {
                this.ListFilm.Add(f);
            }

            foreach(Media s in context.Medias.Where(m => m.TypeMedia.ToString() == "Serie").ToList())
            {
                this.ListSerie.Add(s);
            }

        }

        //Modifier le media choisit par l'utilisateur
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

        //Supprimer le media choisis par l'utilisateur
        public ICommand SupprimerLeMediaCommand
        {
            get
            {
                return _supprimerLeMedia ?? (_supprimerLeMedia = new SupprimerMediaCommand(param => ExecuteMethodSupprimerMedia(param), () => CanExecuteSupprimerLeMedia));
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

            if(mediaASupprimer.TypeMedia.ToString() == "Film")
            {
                this.ListFilm.Remove(mediaASupprimer);
            }
            else
            {
                if(mediaASupprimer.TypeMedia.ToString() == "Serie")
                {
                    this.ListSerie.Remove(mediaASupprimer);
                }
            }

            context.SaveChanges();

        }

        //Ouvrir la fenêtre des films
        public ICommand OuvrirFenetreFilmCommand
        {
            get
            {
                return _ouvrirFenetreFilm ?? (_ouvrirFenetreFilm = new FilmCommand(() => ExecuteMethodOuvrirFenetreFilm(), () => CanExecuteOuvrirFenetreFilm));
            }
        }

        public bool CanExecuteOuvrirFenetreFilm
        {
            get
            {
                return !this.fenetreFilm.IsVisible;
            }
        }

        private void ExecuteMethodOuvrirFenetreFilm()
        {
            this.fenetreFilm = new FilmView(this.ListFilm, this.ListSerie);
            this.fenetreFilm.Show();
        }

        //Ouvrir la fenêtre des séries
        public ICommand OuvrirFenetreSerieCommand
        {
            get
            {
                return _ouvrirFenetreSerie ?? (_ouvrirFenetreSerie = new SerieCommand(() => ExecuteMethodOuvrirFenetreSerie(), () => CanExecuteOuvrirFenetreSerie));
            }
        }

        public bool CanExecuteOuvrirFenetreSerie
        {
            get
            {
                return !this.fenetreSerie.IsVisible;
            }
        }

        private void ExecuteMethodOuvrirFenetreSerie()
        {
            this.fenetreSerie = new SerieView(this.ListSerie, this.ListFilm);
            this.fenetreSerie.Show();
        }
    }
}
