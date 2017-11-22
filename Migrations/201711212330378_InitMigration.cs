namespace WPFCalendarWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Guid(nullable: false),
                        Title = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        AppointmentId = c.Guid(nullable: false),
                        PersonId = c.Guid(nullable: false),
                        accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppointmentId, t.PersonId })
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.AppointmentId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "PersonId", "dbo.People");
            DropForeignKey("dbo.Attendances", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Attendances", new[] { "PersonId" });
            DropIndex("dbo.Attendances", new[] { "AppointmentId" });
            DropTable("dbo.People");
            DropTable("dbo.Attendances");
            DropTable("dbo.Appointments");
        }
    }
}
