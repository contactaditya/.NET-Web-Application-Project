namespace YAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aditya1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
            DropColumn("dbo.AspNetUsers", "RegistrationDate");
            DropColumn("dbo.AspNetUsers", "Deleted");
        }
    }
}
