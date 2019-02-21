namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OperationSetting_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperationSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        enumDays = c.Int(nullable: false),
                        OpenHours = c.Double(nullable: false),
                        ClosingHours = c.Double(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        CompanyId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId_Id)
                .Index(t => t.CompanyId_Id);
            
            AddColumn("dbo.ReservationSetting", "DateTimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReservationSetting", "DateTimeUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationSetting", "CompanyId_Id", "dbo.Company");
            DropIndex("dbo.OperationSetting", new[] { "CompanyId_Id" });
            DropColumn("dbo.ReservationSetting", "DateTimeUpdated");
            DropColumn("dbo.ReservationSetting", "DateTimeCreated");
            DropTable("dbo.OperationSetting");
        }
    }
}
