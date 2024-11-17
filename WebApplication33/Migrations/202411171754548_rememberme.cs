namespace WebApplication33.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rememberme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registrations", "RememberMe", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Registrations", "RememberMe");
        }
    }
}
