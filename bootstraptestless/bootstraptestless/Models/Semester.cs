using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{
    public class Semester
    {
        public virtual int Id { get; set; }
        public virtual string Navn { get; set; }
        public virtual DateTime PeriodeStart { get; set; }
        public virtual DateTime PeriodeSlut { get; set; }
        public virtual int FagId { get; set; }

        [ForeignKey("FagId")]
        public virtual Fag fag { get; set; }

        

        public virtual ICollection<Lektion> Lektioner { get; set; }

    }
}