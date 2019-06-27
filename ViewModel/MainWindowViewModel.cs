using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using videotheque.Commands;
using videotheque.Model;
using videotheque.View;

namespace videotheque.ViewModel
{
    class MainWindowViewModel
    {

        public ObservableCollection<Media> ListFilm { get; set; }
        public ObservableCollection<Media> ListSerie { get; set; }

        public int nbMedia { get; set; }

        public int nbFilms { get; set; }

        public int nbSeries { get; set; }

        private ICommand _modifierLeMediaCommand;

        private ICommand _supprimerLeMedia;

        public MainWindowViewModel()
        {
            this.ListFilm = new ObservableCollection<Media>();

            this.ListSerie = new ObservableCollection<Media>();

            this.LoadData();

            this.nbMedia = this.ListFilm.Count() + this.ListSerie.Count();

            this.nbFilms = this.ListFilm.Count();

            this.nbSeries = this.ListSerie.Count();
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
                return true;
            }
        }

        private void ExecuteMethodModifierMedia(object param)
        {
            ModificationMediaView fenetreModifMedia = new ModificationMediaView(param);
            fenetreModifMedia.Show();
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
            this.SuppressionMediaDb((int)param);

        }

        public async void SuppressionMediaDb(int id)
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            Media mediaASupprimer = context.Medias.Where(m => m.Id == id).First();

            context.Medias.Remove(mediaASupprimer);

            context.SaveChanges();

        }

    }
}
