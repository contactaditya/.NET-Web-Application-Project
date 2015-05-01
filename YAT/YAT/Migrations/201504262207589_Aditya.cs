namespace YAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aditya : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
            AddColumn("dbo.AspNetUsers", "InterestedIn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "InterestedIn");
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
