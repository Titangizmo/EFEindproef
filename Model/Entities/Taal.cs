using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Talen")]
    public class Taal
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int TaalId { get; set; }
        [Required]
        [StringLength(2)]
        public string TaalCode { get; set; }
        [Required]
        [StringLength(20)]
        public string TaalNaam { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Gemeente> Gemeentes { get; set; }
        public virtual ICollection<Persoon> Personen { get; set; }

    }
}
