namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OutletModel_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserExtraInfo", "OutletModel_OutletId", c => c.Long());
            AddColumn("dbo.Outlet", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Outlet", "PhoneNum", c => c.String(maxLength: 15));
            AddColumn("dbo.Outlet", "ContactPersonPhoneNum", c => c.String(maxLength: 15));
            AddColumn("dbo.Outlet", "DateTimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Outlet", "DateTimeUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Outlet", "OperationSettingId_Id", c => c.Long());
            AddColumn("dbo.Outlet", "ReservationSettingId_Id", c => c.Long());
            CreateIndex("dbo.Outlet", "OperationSettingId_Id");
            CreateIndex("dbo.Outlet", "ReservationSettingId_Id");
            CreateIndex("dbo.UserExtraInfo", "OutletModel_OutletId");
            AddForeignKey("dbo.UserExtraInfo", "OutletModel_OutletId", "dbo.Outlet", "OutletId");
            AddForeignKey("dbo.Outlet", "OperationSettingId_Id", "dbo.OperationSetting", "Id");
            AddForeignKey("dbo.Outlet", "ReservationSettingId_Id", "dbo.ReservationSetting", "Id");
            DropColumn("dbo.Outlet", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Outlet", "Active", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Outlet", "ReservationSettingId_Id", "dbo.ReservationSetting");
            DropForeignKey("dbo.Outlet", "OperationSettingId_Id", "dbo.OperationSetting");
            DropForeignKey("dbo.UserExtraInfo", "OutletModel_OutletId", "dbo.Outlet");
            DropIndex("dbo.UserExtraInfo", new[] { "OutletModel_OutletId" });
            DropIndex("dbo.Outlet", new[] { "ReservationSettingId_Id" });
            DropIndex("dbo.Outlet", new[] { "OperationSettingId_Id" });
            DropColumn("dbo.Outlet", "ReservationSettingId_Id");
            DropColumn("dbo.Outlet", "OperationSettingId_Id");
            DropColumn("dbo.Outlet", "DateTimeUpdated");
            DropColumn("dbo.Outlet", "DateTimeCreated");
            DropColumn("dbo.Outlet", "ContactPersonPhoneNum");
            DropColumn("dbo.Outlet", "PhoneNum");
            DropColumn("dbo.Outlet", "isActive");
            DropColumn("dbo.UserExtraInfo", "OutletModel_OutletId");
            DropColumn("dbo.Company", "isActive");
        }
    }
}
