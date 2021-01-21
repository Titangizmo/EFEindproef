using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Berichten")]
   public  class Bericht
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int BerichtId { get; set; }
        
        [Required]
        [ForeignKey(nameof(Gemeente))]
        public int GemeenteId { get; set; }
        [Required]
        [ForeignKey(nameof(Persoon))]
        
        public int PersoonId { get; set; }
        [Required]
        [ForeignKey(nameof(BerichtType))]
        public int BerichtTypeId { get; set; }
        [Required]
        public DateTime BerichtTijdstip { get; set; }
        [Required]
        [StringLength(50)]
        public string BerichtTitel { get; set; }
        [Required]
        [StringLength(255)]
        public string BerichtTekst { get; set; }
        public int? HoofdBerichtId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        [ForeignKey(nameof(PersoonId))]
        public virtual Persoon Persoon { get; set; }
        [ForeignKey(nameof(BerichtTypeId))]
        public virtual BerichtType BerichtType { get; set; }
        public virtual ICollection<Bericht> Berichten { get; set; }
        [InverseProperty("Berichten")]
        [ForeignKey(nameof(HoofdBerichtId))]
        public virtual Bericht HoofdBericht { get; set; }
        [ForeignKey(nameof(GemeenteId))]
        public virtual Gemeente Gemeente { get; set; }
    }
}
