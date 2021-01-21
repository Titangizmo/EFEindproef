using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("InteresseSoorten")]
    public class InteresseSoort
    {   // ----------
        // Properties
        // ----------
        [Key]
        public int IntersseSoortId { get; set; }
        [Required]
        [StringLength(30)]
        public string InteresseSoortNaam { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<ProfielInteresse> ProfielInteresses { get; set; }
    }
}
