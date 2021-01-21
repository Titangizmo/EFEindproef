using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("ProfielInteresses")]
   public class ProfielInteresse
    {
        // ----------
        // Properties
        // ----------
        [Required]
        public int PersoonId { get; set; }
        [Required]
        public int InteresseSoortId { get; set; }
        [StringLength(255)]
        public string ProfielInteresseTekst { get; set; }
        [Timestamp]
        public byte[] Aangepast { get; set; }
    
    // ---------------------
    // Navigation Properties
    // ---------------------
    [ForeignKey("PersoonId")]
        public virtual Persoon Persoon { get; set; }
        [ForeignKey("InteresseSoortId")]
        public virtual InteresseSoort InteresseSoort { get; set; }
    }
}
