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
            this.Lektiontags = new HashSet<Tag>();

        }
        public int Id { get; set; }
        public DateTime Lektionafholdelsestid { get; set; }
        public int Lektionnummer { get; set; }
        public string Lektionsbeskrivelse { get; set; }
        public ICollection<Lektionsfiler> Lektionsfiler { get; set; }
        public ICollection<Lektionsbesvarelser> Lektionsbesvarelser { get; set; }
        public ICollection<Kode> Kodebesvarelser { get; set; }
        public ICollection<Tag> Lektiontags { get; set; }

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