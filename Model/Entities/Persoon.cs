using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    [Table("Personen")]
    public class Persoon
    {
        // ----------
        // Properties
        // ----------
        [Key]
        public int PersoonId { get; set; }
        [Required]
        [StringLength(20)]
        public string VoorNaam { get; set; }
        [Required]
        [StringLength(30)]
        public string FamilieNaam { get; set; }
        
        public enum Geslacht { Man,Vrouw}
        [Required]
        private Geslacht  geslachtValue;

        public Geslacht GeslachtType
        {
            get { return geslachtValue; }
            set { geslachtValue = value; }
        }
        public DateTime GeboorteDatum { get; set; }
        [Required]
        [ForeignKey(nameof(Adres))]
        public int AdresId { get; set; }
        
        [ForeignKey(nameof(Gemeente))]
        public int GeboorteplaatsId { get; set; }
        [Required]
        public bool Geblokkeerd { get; set; }
        [StringLength(30)]
        public string TelefoonNr { get; set; }
        [Required]
        [StringLength(25)]
        public string LoginNaam { get; set; }
        [Required]
        [StringLength(255)]
        public string LoginPaswoord { get; set; }
        [Required]
        public int VerkeerdeLoginsAantal { get; set; }
        [Required]
        public int LoginAantal { get; set; }
        [Required]
        [ForeignKey(nameof(Taal))]
        public int TaalId { get; set; }
        [Timestamp]
        public byte[] Aangepast { get; set; }
    
    // ---------------------
    // Navigation Properties
    // ---------------------
    [ForeignKey(nameof(TaalId))]
        public virtual Taal Taal { get; set; }
        [ForeignKey(nameof(GeboorteplaatsId))]
        
        public virtual  Gemeente Gemeente { get; set; }
        [ForeignKey(nameof(AdresId))]
        public virtual Adres Adres { get; set; }

    }
}
