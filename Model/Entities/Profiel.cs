using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class Profiel: Persoon
    {
        // ----------
        // Properties
        // ----------
        [Required]
        public string KenningsmakingTekst { get; set; }
        public DateTime WoontHierSindsDatum { get; set; }
        [StringLength(30)]
        public string BeroepTekst { get; set; }
        [StringLength(30)]
        public string FirmaNaam { get; set; }
        [StringLength(50)]
        public string WebsiteAdres { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailAdres { get; set; }
        [StringLength(50)]
        public string FacebookNaam { get; set; }
        public DateTime GoedgekeurdTijdstip { get; set; }
        [Required]
        public DateTime CreatieTijdstip { get; set; }
        [Required]
        public DateTime LaatsteUpdateTijdstip { get; set; }
        // ---------------------
        // Navigation Properties
        // ---------------------
        public virtual ICollection<Bericht> Berichten { get; set; }
        public virtual ICollection<ProfielInteresse> ProfielInteresses { get; set; }



    }
}
