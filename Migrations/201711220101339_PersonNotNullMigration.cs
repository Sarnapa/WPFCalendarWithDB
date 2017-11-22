namespace WPFCalendarWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonNotNullMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.People", "UserID", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "UserID", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String(maxLength: 32));
            AlterColumn("dbo.People", "FirstName", c => c.String(maxLength: 32));
        }
    }
}
