using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.Model
{
    public class Episode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdMedia { get; set; }

        public int NumSaison { get; set; }

        public int NumEpisode { get; set; }

        public TimeSpan Duree { get; set; }

        public string Titre { get; set; }

        public string Description { get; set; }

        public DateTime DateDiffusion { get; set; }

        [ForeignKey("IdMedia")]
        public Media Media { get; set; }
    }
}
