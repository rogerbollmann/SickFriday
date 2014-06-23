namespace SickFriday.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        First = c.String(),
                        Second = c.String(),
                        Third = c.String(),
                        Fourth = c.String(),
                        Fifth = c.String(),
                        Sixth = c.String(),
                        Seventh = c.String(),
                        Eight = c.String(),
                        Ninth = c.String(),
                        Date = c.DateTime(nullable: false),
                        BuyIn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
                        Input = c.Int(nullable: false),
                        Win = c.Int(nullable: false),
                        Presence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Rankings");
        }
    }
}
