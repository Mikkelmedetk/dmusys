using bootstraptestless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bootstraptestless.ViewModels
{
    public class KodebesvarelseViewModel
    {
        public Lektion lektion { get; set; }
        public bool Edit { get; set; }
        public Kode KodeBesvarelse { get; set; }
    }
}