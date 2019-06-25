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
    class MainWindowViewModel
    {
        
        public List<Media> ListFilm { get; set; }
        public List<Media> ListSerie { get; set; }

        public int nbMedia { get; set; }

        public int nbFilms { get; set; }

        public int nbSeries { get; set; }

        public MainWindowViewModel()
        {
            this.LoadData();

            ModifierLeMediaCommand = new ModifierMediaCommand(executeMethod, canExecuteMethod);

            this.nbMedia = this.ListFilm.Count() + this.ListSerie.Count();

            this.nbFilms = this.ListFilm.Count();

            this.nbSeries = this.ListSerie.Count();
        }

        internal bool CanExecute()
        {
            return true;
        }

        public async void LoadData()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.ListFilm = context.Medias.Where(m => m.TypeMedia.ToString() == "Film").ToList();

            this.ListSerie = context.Medias.Where(m => m.TypeMedia.ToString() == "Serie").ToList();

        }

        public ICommand ModifierLeMediaCommand
        {
            get;
            set;
        }

        private bool canExecuteMethod()
        {
            return true;
        }

        private void executeMethod()
        {
            Console.WriteLine("No code behind");
        }

    }
}
