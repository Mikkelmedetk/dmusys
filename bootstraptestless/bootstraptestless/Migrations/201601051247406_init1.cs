namespace bootstraptestless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lektion", "Lektionsbeskrivelse", c => c.String(nullable: false, maxLength: 75));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lektion", "Lektionsbeskrivelse", c => c.String());
        }
    }
}
