using Microsoft.EntityFrameworkCore;
using System;
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
        public Media Media { get; set; }

        public int idMedia { get; set; }

        private ICommand _enregistrerModificationMediaCommand;

        private Window modificationVue;

        public ModificationMediaViewModel(){ }

        public ModificationMediaViewModel(int idMedia, Window window)
        {
            this.idMedia = idMedia;
            this.modificationVue = window;
            this.ChargerMediaAModifier();
        }

        public async void ChargerMediaAModifier()
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
            this.modificationVue.Close();
        }

        public async void EnregistrerModificationDb()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            context.Medias.Attach(this.Media);

            context.Entry(this.Media).State = EntityState.Modified;

            context.SaveChanges();

        }

    }

    
}
