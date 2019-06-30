using Microsoft.EntityFrameworkCore;
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
    class ModificationEpisodeViewModel
    {
        public Episode Episode { get; set; }

        private ICommand _enregistrerModificationEpisodeCommand;

        private Window currentWindow;

        public ModificationEpisodeViewModel() { }

        public ModificationEpisodeViewModel(int idSerie, int idEpisode, Window window)
        {
            this.currentWindow = window;
            this.ChargerEpisodeAModifier(idSerie, idEpisode);
        }

        public async void ChargerEpisodeAModifier(int idSerie, int idEpisode)
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Episode = context.Episodes.Where(e => e.Id == idEpisode).Where(e => e.IdMedia == idSerie).First();
        }

        public ICommand EnregistrerModificationEpisode
        {
            get
            {
                return _enregistrerModificationEpisodeCommand ?? (_enregistrerModificationEpisodeCommand = new EnregistrerModificationMediaCommand(() => ExecuteMethodEnregistrerModification(), () => CanExecuteEnregistrerModification));
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
            this.currentWindow.Close();
        }

        public async void EnregistrerModificationDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            context.Episodes.Attach(this.Episode);

            context.Entry(this.Episode).State = EntityState.Modified;

            context.SaveChanges();

        }


    }
}
