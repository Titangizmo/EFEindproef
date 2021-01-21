using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("BerichtTypes")]
    public class BerichtType
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int BerichtTypeId { get; set; }
        [Required]
        [StringLength(2)]
        public string BerichtTypeCode { get; set; }
        [Required]
        [StringLength(20)]
        public string BerichtTypeNaam { get; set; }
        public string BerichtTypeTekst { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Bericht> Berichten { get; set; }

    }
}
