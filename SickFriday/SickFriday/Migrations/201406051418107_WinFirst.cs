namespace SickFriday.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WinFirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rankings", "WinFirst", c => c.Int(nullable: false));
            AddColumn("dbo.Rankings", "WinSecond", c => c.Int(nullable: false));
            AddColumn("dbo.Rankings", "WinThird", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rankings", "WinThird");
            DropColumn("dbo.Rankings", "WinSecond");
            DropColumn("dbo.Rankings", "WinFirst");
        }
    }
}
