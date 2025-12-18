using System;
using System.ComponentModel.DataAnnotations;

namespace rh.Domain.Entities
{
    public class Annonce
    {
        public int Id { get; set; }

        public string Libelle { get; set; }
        public string Description { get; set; }
        public string CompetenceRequis { get; set; }

        // 🔗 Type de contrat
        public int IdTypeContrat { get; set; }
        public TypeContrat? TypeContrat { get; set; }

        // 🔗 Mode de travail
        public int IdModeTravail { get; set; }
        public ModeTravail? ModeTravail { get; set; }

        public string NiveauExperience { get; set; }
        public string Localisation { get; set; }
        public DateTime? DateFin { get; set; }
    }
}
