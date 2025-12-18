using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rh.Domain.Entities
{
    public class Candidature
    {
        public long Id { get; set; }

        // 🔑 Clés étrangères
        public int IdAnnonce { get; set; }
        public long IdCandidat { get; set; }
        public long IdStatut { get; set; }

        // 🔗 Navigation
        public Annonce Annonce { get; set; } = null!;
        public Candidat Candidat { get; set; } = null!;
        public Statut Statut { get; set; } = null!;
    }
}
