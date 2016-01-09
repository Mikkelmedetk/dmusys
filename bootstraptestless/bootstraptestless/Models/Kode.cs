using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{
    public class Kode
    {
        public virtual int Id { get; set; }
        public virtual string Opgavenavn { get; set; }
        public virtual string Indhold { get; set; }

        public virtual int LektionsId { get; set; }

        [ForeignKey("LektionsId")]
        public virtual Lektion Lektion { get; set; }

    }

}