using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{

    public class Lektion
    {
        public Lektion()
        {
            this.Lektionsfiler = new HashSet<Lektionsfiler>();
            this.Lektionsbesvarelser = new HashSet<Lektionsbesvarelser>();
            this.Kodebesvarelser = new HashSet<Kode>();

        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Afholdelsestid skal udfyldes.")]
        public DateTime Lektionafholdelsestid { get; set; }
        [Required(ErrorMessage = "Lektionsnummer skal udfyldes.")]
        public int Lektionnummer { get; set; }
        [Required(ErrorMessage = "Lektionsbeskrivelse skal udfyldes")]
        [StringLength(75, MinimumLength = 5, ErrorMessage = "Skal være imellem 5 og 75 tegn")]
        public string Lektionsbeskrivelse { get; set; }
        public ICollection<Lektionsfiler> Lektionsfiler { get; set; }
        public ICollection<Lektionsbesvarelser> Lektionsbesvarelser { get; set; }
        public ICollection<Kode> Kodebesvarelser { get; set; }

        public int SemesterId { get; set; }

        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }
    }

    public class Lektionsfiler
    {
        public int Id { get; set; }
        public string Filnavn { get; set; }
        public string Url { get; set; }

        public virtual int LektionsId { get; set; }

        [ForeignKey("LektionsId")]
        public virtual Lektion Lektion { get; set; }

    }

    public class Lektionsbesvarelser
    {
        public int Id { get; set; }
        public string Filnavn { get; set; }
        public string Url { get; set; }

        public virtual int LektionsId { get; set; }

        [ForeignKey("LektionsId")]
        public virtual Lektion Lektion { get; set; }

    }
}