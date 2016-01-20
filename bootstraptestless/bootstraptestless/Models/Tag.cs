using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{
    public class Tag
    {

        public Tag()
        {
            this.Lektioner = new HashSet<Lektion>();
        }

        public int Id { get; set; }
        public string tagName { get; set; }

        public ICollection<Lektion> Lektioner { get; set; }
    }
}