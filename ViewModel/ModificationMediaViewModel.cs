using Microsoft.EntityFrameworkCore;
using System;
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
    class ModificationMediaViewModel : UtilsBinding
    {
        public ObservableCollection<Media> ListFilm { get; set; }

        public ObservableCollection<Media> ListSerie { get; set; }

        public Media Media { get; set; }

        private ICommand _enregistrerModificationMediaCommand;

        private Window modificationVue;

        public ModificationMediaViewModel(){ }

        public ModificationMediaViewModel(int idMedia, Window window, ObservableCollection<Media> listFilm, ObservableCollection<Media> listSerie)
        {
            this.modificationVue = window;
            this.ListFilm = listFilm;
            this.ListSerie = listSerie;
            this.ChargerMediaAModifier(idMedia);
        }

        public async void ChargerMediaAModifier(int idMedia)
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Media = context.Medias.Where(m => m.Id == idMedia).First();
        }

        public ICommand EnregistrerModification
        {
            get
            {
                return _enregistrerModificationMediaCommand ?? (_enregistrerModificationMediaCommand = new EnregistrerModificationMediaCommand(()=> ExecuteMethodEnregistrerModification(), () => CanExecuteEnregistrerModification));
            }
        }

        public bool CanExecuteEnregistrerModification
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethodEnregistrerModification()
        {
            this.EnregistrerModificationDb();
            this.ChargerLesList();
            this.modificationVue.Close();
        }

        public async void EnregistrerModificationDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            context.Medias.Attach(this.Media);

            context.Entry(this.Media).State = EntityState.Modified;

            context.SaveChanges();

        }

        public async void ChargerLesList()
        {
            if (this.ListFilm != null)
            {
                this.ListFilm.Clear();
            }
            else
            {
                this.ListFilm = new ObservableCollection<Media>();
                this.ListFilm.Clear();
            }
            if (this.ListSerie != null)
            {
                this.ListSerie.Clear();
            }
            else
            {
                this.ListSerie = new ObservableCollection<Media>();
                this.ListSerie.Clear();
            }

            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            foreach (Media f in context.Medias.Where(m => m.TypeMedia.ToString() == "Film").ToList())
            {
                this.ListFilm.Add(f);
            }

            foreach (Media s in context.Medias.Where(m => m.TypeMedia.ToString() == "Serie").ToList())
            {
                this.ListSerie.Add(s);
            }

        }

    }

    
}
