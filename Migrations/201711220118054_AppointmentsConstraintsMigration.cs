namespace WPFCalendarWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppointmentsConstraintsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Description", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Appointments", "Title", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Appointments", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Appointments", "EndTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "Title", c => c.String());
            DropColumn("dbo.Appointments", "Description");
        }
    }
}
