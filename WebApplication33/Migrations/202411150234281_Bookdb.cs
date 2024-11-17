namespace WebApplication33.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bookdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        PublishingYear = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
