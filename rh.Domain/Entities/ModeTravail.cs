using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rh.Domain.Entities
{
    public class ModeTravail
    {
        public int Id { get; set; }
        public string Libelle { get; set; }

        // Navigation
        public ICollection<Annonce> Annonces { get; set; }
    }
}
