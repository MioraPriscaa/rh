using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rh.Domain.Entities
{
    public class Statut
    {
        public long Id { get; set; }
        public string Libelle { get; set; } = string.Empty;

        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
