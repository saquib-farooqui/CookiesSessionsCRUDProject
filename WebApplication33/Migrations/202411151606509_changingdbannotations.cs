namespace WebApplication33.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingdbannotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "UserId", c => c.String(nullable: false));
        }
    }
}
