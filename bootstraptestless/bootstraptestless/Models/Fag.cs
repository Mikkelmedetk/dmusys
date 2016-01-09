using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{
    public class Fag
    {
        public virtual int Id { get; set; }
        public virtual string Fagnavn { get; set; }
        public virtual string friendlyFagNavn { get; set; }
        public virtual string Fagbeskrivelse { get; set; }
        public virtual ICollection<Semester> Semester { get; set; }
    }
}