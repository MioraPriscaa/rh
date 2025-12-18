using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace rh.Domain.Entities
{
    public class Candidat
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        // CV ou pièce jointe
        public string? PieceJointe { get; set; }

        // 🔗 Navigation
        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
