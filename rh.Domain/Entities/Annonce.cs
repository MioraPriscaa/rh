using System;
using System.ComponentModel.DataAnnotations;

namespace rh.Domain.Entities
{
    public class Annonce
    {
        public int Id { get; set; }

        [Required]
        public string Libelle { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? CompetenceRequis { get; set; }

        // 🔗 Type de contrat
        public int IdTypeContrat { get; set; }
        public TypeContrat? TypeContrat { get; set; }

        // 🔗 Mode de travail
        public int IdModeTravail { get; set; }
        public ModeTravail? ModeTravail { get; set; }

        // 🕒 Durée en mois
        public int Duree { get; set; }

        // 📂 Nombre de dossiers validés (par défaut 0)
        public int NbDossierValide { get; set; } = 0;

        public string? NiveauExperience { get; set; }
        public string? Localisation { get; set; }

        public DateTime? DateFin { get; set; }

        // 🔗 Candidatures
        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
