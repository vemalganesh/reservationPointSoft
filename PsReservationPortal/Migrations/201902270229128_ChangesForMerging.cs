namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesForMerging : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationHourSetting", "Id", "dbo.Outlet");
            DropIndex("dbo.ReservationHourSetting", new[] { "Id" });
            CreateTable(
                "dbo.CustTable",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        RegNumber = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OperationHourSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        StartHour = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        StartMinute = c.Int(nullable: false),
                        EndMinute = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        OperationTypeId_Id = c.Int(nullable: false),
                        OutletId_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OperationType", t => t.OperationTypeId_Id, cascadeDelete: true)
                .ForeignKey("dbo.Outlet", t => t.OutletId_Id, cascadeDelete: true)
                .Index(t => t.OperationTypeId_Id)
                .Index(t => t.OutletId_Id);
            
            CreateTable(
                "dbo.OperationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        isAllowReserve = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserExtraInfo", "CustTableModel_Id", c => c.Long());
            AddColumn("dbo.Outlet", "CustTableModel_Id", c => c.Long());
            CreateIndex("dbo.UserExtraInfo", "CustTableModel_Id");
            CreateIndex("dbo.Outlet", "CustTableModel_Id");
            AddForeignKey("dbo.Outlet", "CustTableModel_Id", "dbo.CustTable", "Id");
            AddForeignKey("dbo.UserExtraInfo", "CustTableModel_Id", "dbo.CustTable", "Id");
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
            
            DropForeignKey("dbo.UserExtraInfo", "CustTableModel_Id", "dbo.CustTable");
            DropForeignKey("dbo.Outlet", "CustTableModel_Id", "dbo.CustTable");
            DropForeignKey("dbo.OperationHourSetting", "OutletId_Id", "dbo.Outlet");
            DropForeignKey("dbo.OperationHourSetting", "OperationTypeId_Id", "dbo.OperationType");
            DropIndex("dbo.OperationHourSetting", new[] { "OutletId_Id" });
            DropIndex("dbo.OperationHourSetting", new[] { "OperationTypeId_Id" });
            DropIndex("dbo.Outlet", new[] { "CustTableModel_Id" });
            DropIndex("dbo.UserExtraInfo", new[] { "CustTableModel_Id" });
            DropColumn("dbo.Outlet", "CustTableModel_Id");
            DropColumn("dbo.UserExtraInfo", "CustTableModel_Id");
            DropTable("dbo.OperationType");
            DropTable("dbo.OperationHourSetting");
            DropTable("dbo.CustTable");
            CreateIndex("dbo.ReservationHourSetting", "Id");
            AddForeignKey("dbo.ReservationHourSetting", "Id", "dbo.Outlet", "Id");
        }
    }
}
