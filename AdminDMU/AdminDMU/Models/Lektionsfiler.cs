//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminDMU.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lektionsfiler
    {
        public int Id { get; set; }
        public string Filnavn { get; set; }
        public string Url { get; set; }
        public int LektionsId { get; set; }
    
        public virtual Lektion Lektion { get; set; }
    }
}