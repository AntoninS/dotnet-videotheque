using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using videotheque.Model;

namespace videotheque.ViewModel
{
    class MainWindowViewModel
    {
        
        public List<Media> Medias { get; set; }

        public MainWindowViewModel()
        {
            this.LoadMedia();   
        }

        public async Task LoadMedia()
        {
            var context = await DataAccess.VideothequeDbContext.GetCurrent();

            this.Medias = context.Medias.ToList();

        }

    }
}
