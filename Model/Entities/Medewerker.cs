using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
    public class Medewerker: Persoon
    {
        // ----------
        // Properties
        // ----------
        [Required]
        public int AfdelingsId { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual Afdeling Afdeling { get; set; }

    }
}
