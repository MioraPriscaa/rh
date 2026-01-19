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

        public int IdTypeContrat { get; set; }
        public TypeContrat? TypeContrat { get; set; }

        public int IdModeTravail { get; set; }
        public ModeTravail? ModeTravail { get; set; }

        public int Duree { get; set; }

        public int NbDossierValide { get; set; } = 0;

        public string? NiveauExperience { get; set; }
        public string? Localisation { get; set; }

        public DateTime? DateFin { get; set; }

        public DateTime DateCreation { get; set; }

        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
