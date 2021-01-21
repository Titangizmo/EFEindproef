using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Provincies")]
    public class Provincie
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int ProvincieId { get; set; }
        [Required]
        [StringLength(3)]
        public string ProvincieCode { get; set; }
        [Required]
        [StringLength(30)]
        public string ProvincieNaam { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Gemeente> Gemeentes { get; set; }
    }
}
