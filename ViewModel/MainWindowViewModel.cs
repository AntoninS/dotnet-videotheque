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

        private ICommand _modifierLeMediaCommand;

        public MainWindowViewModel()
        {
            this.LoadData();

            this.nbMedia = this.ListFilm.Count() + this.ListSerie.Count();

            this.nbFilms = this.ListFilm.Count();

            this.nbSeries = this.ListSerie.Count();
        }

        public async void LoadData()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.ListFilm = context.Medias.Where(m => m.TypeMedia.ToString() == "Film").ToList();

            this.ListSerie = context.Medias.Where(m => m.TypeMedia.ToString() == "Serie").ToList();

        }

        public ICommand ModifierLeMediaCommand
        {
            get
            {
                return _modifierLeMediaCommand ?? (_modifierLeMediaCommand = new ModifierMediaCommand(param => ExecuteMethod(param), () => CanExecute));
            }
        }

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        private void ExecuteMethod(object param)
        {
            Console.WriteLine("No code behind");
            Console.WriteLine(param);
        }

    }
}
