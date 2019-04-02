using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.classes
{
    class Episode
    {
        public int Id { get; set; }

        public int IdMedia { get; set; }

        public int NumSaison { get; set; }

        public int NumEpisode { get; set; }

        public TimeSpan Duree { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public DateTime DateDiffusion { get; set; }
    }
}
