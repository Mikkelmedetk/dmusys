namespace bootstraptestless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fagnavn = c.String(),
                        friendlyFagNavn = c.String(),
                        Fagbeskrivelse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Semester",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        PeriodeStart = c.DateTime(nullable: false),
                        PeriodeSlut = c.DateTime(nullable: false),
                        FagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fag", t => t.FagId, cascadeDelete: true)
                .Index(t => t.FagId);
            
            CreateTable(
                "dbo.Lektion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lektionafholdelsestid = c.DateTime(nullable: false),
                        Lektionnummer = c.Int(nullable: false),
                        Lektionsbeskrivelse = c.String(),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semester", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Lektionsbesvarelser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filnavn = c.String(),
                        Url = c.String(),
                        LektionsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lektion", t => t.LektionsId, cascadeDelete: true)
                .Index(t => t.LektionsId);
            
            CreateTable(
                "dbo.Lektionsfiler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filnavn = c.String(),
                        Url = c.String(),
                        LektionsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lektion", t => t.LektionsId, cascadeDelete: true)
                .Index(t => t.LektionsId);
            
            CreateTable(
                "dbo.Kode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Indhold = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KodeFiler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filnavn = c.String(),
                        KodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kode", t => t.KodeId, cascadeDelete: true)
                .Index(t => t.KodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KodeFiler", "KodeId", "dbo.Kode");
            DropForeignKey("dbo.Lektion", "SemesterId", "dbo.Semester");
            DropForeignKey("dbo.Lektionsfiler", "LektionsId", "dbo.Lektion");
            DropForeignKey("dbo.Lektionsbesvarelser", "LektionsId", "dbo.Lektion");
            DropForeignKey("dbo.Semester", "FagId", "dbo.Fag");
            DropIndex("dbo.KodeFiler", new[] { "KodeId" });
            DropIndex("dbo.Lektionsfiler", new[] { "LektionsId" });
            DropIndex("dbo.Lektionsbesvarelser", new[] { "LektionsId" });
            DropIndex("dbo.Lektion", new[] { "SemesterId" });
            DropIndex("dbo.Semester", new[] { "FagId" });
            DropTable("dbo.KodeFiler");
            DropTable("dbo.Kode");
            DropTable("dbo.Lektionsfiler");
            DropTable("dbo.Lektionsbesvarelser");
            DropTable("dbo.Lektion");
            DropTable("dbo.Semester");
            DropTable("dbo.Fag");
        }
    }
}
