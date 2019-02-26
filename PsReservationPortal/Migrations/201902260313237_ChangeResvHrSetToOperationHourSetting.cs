namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeResvHrSetToOperationHourSetting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationHourSetting", "Id", "dbo.Outlet");
            DropIndex("dbo.ReservationHourSetting", new[] { "Id" });
            CreateTable(
                "dbo.OperationHourSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        day = c.Int(nullable: false),
                        OpenHour = c.Double(nullable: false),
                        CloseHour = c.Double(nullable: false),
                        StartBreak = c.Double(nullable: false),
                        EndBreak = c.Double(nullable: false),
                        StartResvPeriod = c.Double(nullable: false),
                        EndResvPeriod = c.Double(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        OutletId_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outlet", t => t.OutletId_Id)
                .Index(t => t.OutletId_Id);
            
            DropTable("dbo.ReservationHourSetting");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReservationHourSetting",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        MonStartTime = c.Double(nullable: false),
                        MonEndTime = c.Double(nullable: false),
                        TueStartTime = c.Double(nullable: false),
                        TueEndTime = c.Double(nullable: false),
                        WedStartTime = c.Double(nullable: false),
                        WedEndTime = c.Double(nullable: false),
                        ThurStartTime = c.Double(nullable: false),
                        ThurEndTime = c.Double(nullable: false),
                        FriStartTime = c.Double(nullable: false),
                        FriEndTime = c.Double(nullable: false),
                        SatStartTime = c.Double(nullable: false),
                        SatEndTime = c.Double(nullable: false),
                        SunStartTime = c.Double(nullable: false),
                        SunEndTime = c.Double(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OperationHourSetting", "OutletId_Id", "dbo.Outlet");
            DropIndex("dbo.OperationHourSetting", new[] { "OutletId_Id" });
            DropTable("dbo.OperationHourSetting");
            CreateIndex("dbo.ReservationHourSetting", "Id");
            AddForeignKey("dbo.ReservationHourSetting", "Id", "dbo.Outlet", "Id");
        }
    }
}
