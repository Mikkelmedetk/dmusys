using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace bootstraptestless.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DMUSys")
        {

        }
        public DbSet<Fag> _Fag { get; set; }
        public DbSet<Semester> _Semester { get; set; }
        public DbSet<Lektion> _Lektion { get; set; }
        public DbSet<Lektionsfiler> _Lektionsfiler { get; set; }
        public DbSet<Lektionsbesvarelser> _Lektionsbesvarelser { get; set; }
        public DbSet<Kode> _Kode { get; set; }
        public DbSet<Tag> _Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Lektion>()
                .HasMany<Tag>(t => t.Lektiontags)
                .WithMany(l => l.Lektioner)
                .Map(lt =>
                {
                    lt.MapLeftKey("LektionsId");
                    lt.MapRightKey("TagId");
                    lt.ToTable("LektionToTag");
                });
        }
    }
}