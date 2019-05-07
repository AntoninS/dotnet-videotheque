using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.classes
{
    public class Media
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateSortie { get; set; }

        public TimeSpan Duree { get; set; }

        public string Titre { get; set; }

        public bool Vu { get; set; }

        public int Note { get; set; }

        public string Commentaire { get; set; }

        public string Synopsis { get; set; }

        public ETypeMedia TypeMedia { get; set; }

        public int AgeMinimum { get; set; }

        public bool SupportPhysique { get; set; }

        public bool SupportNumerique { get; set; }

        public string Image { get; set; }

        public ELangue LangueVO { get; set; }

        public ELangue LangueMedia { get; set; }

        public ELangue SousTitres { get; set; }

        [InverseProperty(nameof(GenreMedia.Media))]
        public List<GenreMedia> Genres { get; set; }

        [InverseProperty(nameof(PersonneMedia.Media))]
        public List<PersonneMedia> Personnes { get; set; }

        [InverseProperty(nameof(Episode.Media))]
        public List<Episode> Episodes { get; set; }
    }
}
