using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Afdelingen")]
   public  class Afdeling
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int AfdelingId { get; set; }
        [Required]
        [StringLength(4)]
        public string AfdelingCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AfdelingNaam { get; set; }
        [StringLength(255)]
        public string AfdelingTekst { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Medewerker> Medewerkers { get; set; }

    }
}
