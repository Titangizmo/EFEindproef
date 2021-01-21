using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Straten")]
    public class Straat
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int StraatId { get; set; }
        [Required]
        [StringLength(50)]
        public string StraatNaam { get; set; }
        [ForeignKey(nameof(Gemeente))]
        [Required]
        public int GemeenteId { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        [ForeignKey(nameof(GemeenteId))]
        public virtual Gemeente Gemeente { get; set; }
        public virtual ICollection<Adres> Adressen { get; set; }


    }
}
