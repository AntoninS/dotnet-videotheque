using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.classes
{
    class PersonneMedia
    {
        public int Id { get; set; }

        public int IdMedia { get; set; }

        public int IdPersonne { get; set; }

        public EFonction Fonction { get; set; }

        public string Role { get; set; }
    }
}
