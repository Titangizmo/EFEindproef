using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Adressen")]
   public class Adres
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int AdresId { get; set; }
        [Required]
        [StringLength(5)]
        public string  HuisNr { get; set; }
        [StringLength(5)]
        public string BusNr { get; set; }
        [ForeignKey(nameof(Straat))]
        [Required]
        public int StraatId { get; set; }
        [Timestamp]
        public byte[] Aangepast { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        [ForeignKey(nameof(StraatId))]
        public virtual Straat Straat { get; set; }
        public virtual ICollection<Persoon> Personen { get; set; }

    }
}
