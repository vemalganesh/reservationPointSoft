namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_Changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OperationSetting", "CompanyId_Id", "dbo.Company");
            DropForeignKey("dbo.Outlet", "OperationSettingId_Id", "dbo.OperationSetting");
            DropForeignKey("dbo.ReservationSetting", "CompanyId_Id", "dbo.Company");
            DropForeignKey("dbo.Outlet", "ReservationSettingId_Id", "dbo.ReservationSetting");
            DropForeignKey("dbo.OutletOffDay", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.OutletWorkingDate", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.UserExtraInfo", "OutletModel_OutletId", "dbo.Outlet");
            DropForeignKey("dbo.Table", "OutletId_OutletId", "dbo.Outlet");
            DropIndex("dbo.OperationSetting", new[] { "CompanyId_Id" });
            DropIndex("dbo.Outlet", new[] { "OperationSettingId_Id" });
            DropIndex("dbo.Outlet", new[] { "ReservationSettingId_Id" });
            DropIndex("dbo.ReservationSetting", new[] { "CompanyId_Id" });
            DropIndex("dbo.OutletOffDay", new[] { "Company_Id" });
            DropIndex("dbo.OutletWorkingDate", new[] { "Company_Id" });
            RenameColumn(table: "dbo.UserExtraInfo", name: "OutletModel_OutletId", newName: "OutletModel_Id");
            RenameColumn(table: "dbo.Table", name: "OutletId_OutletId", newName: "OutletId_Id");
            RenameIndex(table: "dbo.UserExtraInfo", name: "IX_OutletModel_OutletId", newName: "IX_OutletModel_Id");
            RenameIndex(table: "dbo.Table", name: "IX_OutletId_OutletId", newName: "IX_OutletId_Id");
            DropPrimaryKey("dbo.Outlet");
            DropColumn("dbo.Outlet", "OutletId");
            AddColumn("dbo.Outlet", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Outlet", "Id");
            CreateTable(
                "dbo.CloseDateSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Remark = c.String(),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        OutletId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outlet", t => t.OutletId_Id)
                .Index(t => t.OutletId_Id);
            
            CreateTable(
                "dbo.ReservationDaySetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonOff = c.Boolean(nullable: false),
                        TueOff = c.Boolean(nullable: false),
                        WedOff = c.Boolean(nullable: false),
                        ThuOff = c.Boolean(nullable: false),
                        FriOff = c.Boolean(nullable: false),
                        SatOff = c.Boolean(nullable: false),
                        SunOff = c.Boolean(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        OutletId_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outlet", t => t.OutletId_Id)
                .Index(t => t.OutletId_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outlet", t => t.Id)
                .Index(t => t.Id);
            
            
            AddColumn("dbo.Outlet", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Outlet", "Location", c => c.String());
            AddColumn("dbo.Outlet", "Address", c => c.String());
            AddColumn("dbo.Outlet", "Description", c => c.String());
            AddColumn("dbo.Outlet", "ReservationDuration", c => c.Double(nullable: false));
            AddColumn("dbo.Outlet", "ReservationAllowBefore", c => c.Double(nullable: false));
            AddForeignKey("dbo.UserExtraInfo", "OutletModel_Id", "dbo.Outlet", "Id");
            AddForeignKey("dbo.Table", "OutletId_Id", "dbo.Outlet", "Id");
            DropColumn("dbo.Outlet", "OutletName");
            DropColumn("dbo.Outlet", "OutletLocation");
            DropColumn("dbo.Outlet", "OutletAddress");
            DropColumn("dbo.Outlet", "OperationSettingId_Id");
            DropColumn("dbo.Outlet", "ReservationSettingId_Id");
            DropTable("dbo.OperationSetting");
            DropTable("dbo.ReservationSetting");
            DropTable("dbo.OutletOffDay");
            DropTable("dbo.OutletWorkingDate");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OutletWorkingDate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutletId = c.Int(nullable: false),
                        BusinessDateFrom = c.DateTime(nullable: false),
                        BusinessDateTo = c.DateTime(nullable: false),
                        WorkDateStatus = c.Boolean(nullable: false),
                        ModifiedDateTime = c.DateTime(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        CreateddDateTime = c.DateTime(nullable: false),
                        Company_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OutletOffDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutletId = c.Int(nullable: false),
                        MonOff = c.Boolean(nullable: false),
                        TueOff = c.Boolean(nullable: false),
                        WedOff = c.Boolean(nullable: false),
                        ThuOff = c.Boolean(nullable: false),
                        FriOff = c.Boolean(nullable: false),
                        SatOff = c.Boolean(nullable: false),
                        SunOff = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ModifiedDateTime = c.DateTime(nullable: false),
                        ModifiedUserId = c.Int(nullable: false),
                        Company_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReservationSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ReservationDuration = c.Double(nullable: false),
                        ReservationAllowBefore = c.Double(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        CompanyId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Outlet", "ReservationSettingId_Id", c => c.Long());
            AddColumn("dbo.Outlet", "OperationSettingId_Id", c => c.Long());
            AddColumn("dbo.Outlet", "OutletAddress", c => c.String());
            AddColumn("dbo.Outlet", "OutletLocation", c => c.String());
            AddColumn("dbo.Outlet", "OutletName", c => c.String(nullable: false));
            AddColumn("dbo.Outlet", "OutletId", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.Table", "OutletId_Id", "dbo.Outlet");
            DropForeignKey("dbo.UserExtraInfo", "OutletModel_Id", "dbo.Outlet");
            DropForeignKey("dbo.CloseDateSetting", "OutletId_Id", "dbo.Outlet");
            DropForeignKey("dbo.ReservationHourSetting", "Id", "dbo.Outlet");
            DropForeignKey("dbo.ReservationDaySetting", "OutletId_Id", "dbo.Outlet");
            DropIndex("dbo.ReservationHourSetting", new[] { "Id" });
            DropIndex("dbo.ReservationDaySetting", new[] { "OutletId_Id" });
            DropIndex("dbo.CloseDateSetting", new[] { "OutletId_Id" });
            DropPrimaryKey("dbo.Outlet");
            DropColumn("dbo.Outlet", "ReservationAllowBefore");
            DropColumn("dbo.Outlet", "ReservationDuration");
            DropColumn("dbo.Outlet", "Description");
            DropColumn("dbo.Outlet", "Address");
            DropColumn("dbo.Outlet", "Location");
            DropColumn("dbo.Outlet", "Name");
            DropColumn("dbo.Outlet", "Id");
            DropTable("dbo.ReservationHourSetting");
            DropTable("dbo.ReservationDaySetting");
            DropTable("dbo.CloseDateSetting");
            AddPrimaryKey("dbo.Outlet", "OutletId");
            RenameIndex(table: "dbo.Table", name: "IX_OutletId_Id", newName: "IX_OutletId_OutletId");
            RenameIndex(table: "dbo.UserExtraInfo", name: "IX_OutletModel_Id", newName: "IX_OutletModel_OutletId");
            RenameColumn(table: "dbo.Table", name: "OutletId_Id", newName: "OutletId_OutletId");
            RenameColumn(table: "dbo.UserExtraInfo", name: "OutletModel_Id", newName: "OutletModel_OutletId");
            CreateIndex("dbo.OutletWorkingDate", "Company_Id");
            CreateIndex("dbo.OutletOffDay", "Company_Id");
            CreateIndex("dbo.ReservationSetting", "CompanyId_Id");
            CreateIndex("dbo.Outlet", "ReservationSettingId_Id");
            CreateIndex("dbo.Outlet", "OperationSettingId_Id");
            CreateIndex("dbo.OperationSetting", "CompanyId_Id");
            AddForeignKey("dbo.Table", "OutletId_OutletId", "dbo.Outlet", "OutletId");
            AddForeignKey("dbo.UserExtraInfo", "OutletModel_OutletId", "dbo.Outlet", "OutletId");
            AddForeignKey("dbo.OutletWorkingDate", "Company_Id", "dbo.Company", "Id");
            AddForeignKey("dbo.OutletOffDay", "Company_Id", "dbo.Company", "Id");
            AddForeignKey("dbo.Outlet", "ReservationSettingId_Id", "dbo.ReservationSetting", "Id");
            AddForeignKey("dbo.ReservationSetting", "CompanyId_Id", "dbo.Company", "Id");
            AddForeignKey("dbo.Outlet", "OperationSettingId_Id", "dbo.OperationSetting", "Id");
            AddForeignKey("dbo.OperationSetting", "CompanyId_Id", "dbo.Company", "Id");
        }
    }
}
