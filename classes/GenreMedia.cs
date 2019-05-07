using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.classes
{
    public class GenreMedia
    {
        public int IdGenre { get; set; }

        public int IdMedia { get; set; }

        [ForeignKey("IdGenre")]
        public Genre Genre { get; set; }

        [ForeignKey("IdMedia")]
        public Media Media { get; set; }
    }
}
