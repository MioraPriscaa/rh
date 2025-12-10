using System;
using System.ComponentModel.DataAnnotations;

namespace rh.Domain.Entities
{
    public class Annonce
    {
        public int id { get; set; }

        [Required]
        public string nom { get; set; }

        public DateTime date { get; set; }
    }
}
