using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Gemeentes")]
    public class Gemeente
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int GemeenteId { get; set; }
        [Required]
        [StringLength(50)]
        public string GemeenteNaam { get; set; }
        [Required]
        public int PostCode { get; set; }
        [ForeignKey(nameof(Provincie))]
        [Required]
        public int ProvincieId { get; set; }
        [ForeignKey(nameof(Taal))]
        [Required]
        public int TaalId { get; set; }
        public int? HoofdGemeenteId { get; set; }

        // ---------------------
        // Navigation Properties
        // ---------------------
        [ForeignKey(nameof(ProvincieId))]
        public virtual Provincie Provincie { get; set; }
        [ForeignKey(nameof(TaalId))]
        public virtual Taal Taal { get; set; }
        public virtual ICollection<Straat> Straten { get; set; }
        public virtual ICollection<Persoon> Personen { get; set; }
        public virtual ICollection<Gemeente> Gemeentes{ get; set; }
        [InverseProperty("Gemeentes")]
        [ForeignKey("HoofdGemeenteId")]
        public virtual Gemeente HoofdGemeente { get; set; }

    }
}
