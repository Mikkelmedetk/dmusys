namespace bootstraptestless.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<bootstraptestless.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "bootstraptestless.Models.DataContext";
        }

        protected override void Seed(bootstraptestless.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context._Fag.AddOrUpdate(
                f => f.Fagnavn,
                new Fag { Fagnavn = "Programmering", friendlyFagNavn = "programmering", Fagbeskrivelse = "Indeholder opgaver", Semester =
                    new List<Semester> { 
                    new Semester { Navn = "1", PeriodeStart = DateTime.Now, PeriodeSlut = DateTime.Now.AddMonths(4), Lektioner =
                    new List<Lektion>
                    {
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 1, Lektionsbeskrivelse = "Blablabla" },
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 2, Lektionsbeskrivelse = "Blablabla" },
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 3, Lektionsbeskrivelse = "Blablabla" }
                    }
                    },
                        new Semester { Navn = "2", PeriodeStart = DateTime.Now, PeriodeSlut = DateTime.Now.AddMonths(4), Lektioner =
                    new List<Lektion>
                    {
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 1, Lektionsbeskrivelse = "Blablabla" }
                    }

                    }
                    }

                }
                );

            context._Fag.AddOrUpdate(
                f => f.Fagnavn,
                new Fag
                {
                    Fagnavn = "Forretning og it",
                    friendlyFagNavn = "forretning-og-it",
                    Fagbeskrivelse = "Indeholder opgaver",
                    Semester =
                    new List<Semester> {
                    new Semester { Navn = "1", PeriodeStart = DateTime.Now, PeriodeSlut = DateTime.Now.AddMonths(4), Lektioner =
                    new List<Lektion>
                    {
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 1, Lektionsbeskrivelse = "Blablabla" },
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 2, Lektionsbeskrivelse = "Blablabla" },
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 3, Lektionsbeskrivelse = "Blablabla" }
                    }
                    },
                        new Semester { Navn = "2", PeriodeStart = DateTime.Now, PeriodeSlut = DateTime.Now.AddMonths(4), Lektioner =
                    new List<Lektion>
                    {
                        new Lektion { Lektionafholdelsestid = DateTime.Now, Lektionnummer = 1, Lektionsbeskrivelse = "Blablabla", Lektionsfiler =
                        new List<Lektionsfiler> {
                            new Lektionsfiler { Filnavn = "Opgave1", Url ="#" },
                            new Lektionsfiler { Filnavn = "Opgave2", Url ="#" }
                        }, Lektionsbesvarelser =
                        new List<Lektionsbesvarelser>
                        {
                            new Lektionsbesvarelser { Filnavn="Opgave1", Url = "#" }
                        } }
                    }

                    }
                    }

                }
                );



        }
    }
}
