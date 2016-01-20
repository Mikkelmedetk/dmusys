using bootstraptestless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bootstraptestless.ViewModels
{
    public class HomeViewModel
    {
        public Fag valgtFag { get; set; }
        public Semester valgtSemester { get; set; }
        public ICollection<Lektion> relateredeLektioner { get; set; }

        public IEnumerable<SelectListItem> ddlSemestre { get; set; }

        public Lektion valgtLektion { get; set; }
        public ICollection<Lektionsfiler> lektionsfiler { get; set; }
        public ICollection<Lektionsbesvarelser> lektionsbesvarelser { get; set; }
        public ICollection<Kode> kodebesvarelser { get; set; }
        public ICollection<Tag> lektiontags { get; set; }

        public Lektion opretLektion { get; set; }
        public Lektionsfiler opretLektionsfil { get; set; }
        public Lektionsbesvarelser opretLektionsbesvarelse { get; set; }
        public Kode opretKodebesvarelse { get; set; }
        public Tag opretTag { get; set; }
    }
}