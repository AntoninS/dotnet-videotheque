using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.Model
{
    public class GenreMedia
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGenre { get; set; }

        [ForeignKey("IdGenre")]
        public Genre Genre { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMedia { get; set; }

        [ForeignKey("IdMedia")]
        public Media Media { get; set; }
    }
}
