using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    class FilmViewModel : UtilsBinding
    {
        public ObservableCollection<Media> ListFilm { get; set; }

        private ICommand _modifierLeMediaCommand;

        private ICommand _supprimerLeMedia;

        private ICommand _ajouterFilmCommand;

        private Media film;

        public Media Film { get => film; set => SetProperty(ref film, value); }

        public FilmViewModel()
        {
            this.ListFilm = new ObservableCollection<Media>();

            this.LoadData();
        }

        public async void LoadData()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            foreach (Media f in context.Medias.Where(m => m.TypeMedia.ToString() == "Film").ToList())
            {
                this.ListFilm.Add(f);
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

            ListFilm.Remove(Film);

            context.SaveChanges();

        }

        //Ajouter un film
        public ICommand AjouterFilmCommand
        {
            get
            {
                return _ajouterFilmCommand ?? (_ajouterFilmCommand = new FilmCommand(() => ExecuteMethodAjouterFilm(), () => CanExecuteAjouterFilm));
            }
        }

        public bool CanExecuteAjouterFilm
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodAjouterFilm()
        {
            AjouterFilmView ajouterFilmView = new AjouterFilmView();
            ajouterFilmView.Show();
        }
    }
    
}
