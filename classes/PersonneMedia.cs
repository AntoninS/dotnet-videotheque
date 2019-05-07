using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.classes
{
    public class PersonneMedia
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdMedia { get; set; }

        public int IdPersonne { get; set; }

        public EFonction.Fonction Fonction { get; set; }

        public string Role { get; set; }

        [ForeignKey("IdMedia")]
        public Media Media { get; set; }

        [ForeignKey("IdPersonne")]
        public Personne Personne { get; set; }
    }
}
