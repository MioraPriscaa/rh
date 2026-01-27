using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public double? ScoreCorrespondance { get; set; }
        // 🔗 Navigation
        [NotMapped]
        public Annonce Annonce { get; set; } = null!;
        [NotMapped]
        public Candidat Candidat { get; set; } = null!;
        [NotMapped]
        public Statut Statut { get; set; } = null!;
        
    }
}
