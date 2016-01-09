namespace bootstraptestless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KodeFiler", "KodeId", "dbo.Kode");
            DropIndex("dbo.KodeFiler", new[] { "KodeId" });
            AddColumn("dbo.Kode", "Opgavenavn", c => c.String());
            AddColumn("dbo.Kode", "LektionsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Kode", "LektionsId");
            AddForeignKey("dbo.Kode", "LektionsId", "dbo.Lektion", "Id", cascadeDelete: true);
            DropTable("dbo.KodeFiler");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KodeFiler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Filnavn = c.String(),
                        KodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Kode", "LektionsId", "dbo.Lektion");
            DropIndex("dbo.Kode", new[] { "LektionsId" });
            DropColumn("dbo.Kode", "LektionsId");
            DropColumn("dbo.Kode", "Opgavenavn");
            CreateIndex("dbo.KodeFiler", "KodeId");
            AddForeignKey("dbo.KodeFiler", "KodeId", "dbo.Kode", "Id", cascadeDelete: true);
        }
    }
}
