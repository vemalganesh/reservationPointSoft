namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Outlet", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.ReservationExclusionDate", "OutletId_Id", "dbo.Outlet");
            DropForeignKey("dbo.Table", "OutletId_Id", "dbo.Outlet");
            DropIndex("dbo.Outlet", new[] { "Company_Id" });
            DropIndex("dbo.ReservationExclusionDay", new[] { "OutletId_Id" });
            DropIndex("dbo.ReservationExclusionDate", new[] { "OutletId_Id" });
            DropIndex("dbo.Table", new[] { "OutletId_Id" });
            DropPrimaryKey("dbo.ReservationExclusionDay");
            DropColumn("dbo.ReservationExclusionDay", "Id");
            RenameColumn(table: "dbo.Outlet", name: "Company_Id", newName: "CompanyId");
            RenameColumn(table: "dbo.OperationHourSetting", name: "OutletId_Id", newName: "OutletId");
            RenameColumn(table: "dbo.ReservationExclusionDate", name: "OutletId_Id", newName: "OutletId");
            RenameColumn(table: "dbo.Table", name: "OutletId_Id", newName: "OutletId");
            RenameColumn(table: "dbo.OperationHourSetting", name: "OperationTypeId_Id", newName: "OperationTypeId");
            RenameColumn(table: "dbo.ReservationExclusionDay", name: "OutletId_Id", newName: "Id");
            RenameIndex(table: "dbo.OperationHourSetting", name: "IX_OutletId_Id", newName: "IX_OutletId");
            RenameIndex(table: "dbo.OperationHourSetting", name: "IX_OperationTypeId_Id", newName: "IX_OperationTypeId");
            AlterColumn("dbo.Outlet", "CompanyId", c => c.Long(nullable: false));
            AlterColumn("dbo.ReservationExclusionDay", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.ReservationExclusionDate", "OutletId", c => c.Long(nullable: false));
            AlterColumn("dbo.Table", "OutletId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.ReservationExclusionDay", "Id");
            CreateIndex("dbo.Outlet", "CompanyId");
            CreateIndex("dbo.ReservationExclusionDate", "OutletId");
            CreateIndex("dbo.ReservationExclusionDay", "Id");
            CreateIndex("dbo.Table", "OutletId");
            AddForeignKey("dbo.Outlet", "CompanyId", "dbo.Company", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationExclusionDate", "OutletId", "dbo.Outlet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Table", "OutletId", "dbo.Outlet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Table", "OutletId", "dbo.Outlet");
            DropForeignKey("dbo.ReservationExclusionDate", "OutletId", "dbo.Outlet");
            DropForeignKey("dbo.Outlet", "CompanyId", "dbo.Company");
            DropIndex("dbo.Table", new[] { "OutletId" });
            DropIndex("dbo.ReservationExclusionDay", new[] { "Id" });
            DropIndex("dbo.ReservationExclusionDate", new[] { "OutletId" });
            DropIndex("dbo.Outlet", new[] { "CompanyId" });
            DropPrimaryKey("dbo.ReservationExclusionDay");
            AlterColumn("dbo.Table", "OutletId", c => c.Long());
            AlterColumn("dbo.ReservationExclusionDate", "OutletId", c => c.Long());
            AlterColumn("dbo.ReservationExclusionDay", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Outlet", "CompanyId", c => c.Long());
            AddPrimaryKey("dbo.ReservationExclusionDay", "Id");
            RenameIndex(table: "dbo.OperationHourSetting", name: "IX_OperationTypeId", newName: "IX_OperationTypeId_Id");
            RenameIndex(table: "dbo.OperationHourSetting", name: "IX_OutletId", newName: "IX_OutletId_Id");
            RenameColumn(table: "dbo.ReservationExclusionDay", name: "Id", newName: "OutletId_Id");
            RenameColumn(table: "dbo.OperationHourSetting", name: "OperationTypeId", newName: "OperationTypeId_Id");
            RenameColumn(table: "dbo.Table", name: "OutletId", newName: "OutletId_Id");
            RenameColumn(table: "dbo.ReservationExclusionDate", name: "OutletId", newName: "OutletId_Id");
            RenameColumn(table: "dbo.OperationHourSetting", name: "OutletId", newName: "OutletId_Id");
            RenameColumn(table: "dbo.Outlet", name: "CompanyId", newName: "Company_Id");
            AddColumn("dbo.ReservationExclusionDay", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Table", "OutletId_Id");
            CreateIndex("dbo.ReservationExclusionDate", "OutletId_Id");
            CreateIndex("dbo.ReservationExclusionDay", "OutletId_Id");
            CreateIndex("dbo.Outlet", "Company_Id");
            AddForeignKey("dbo.Table", "OutletId_Id", "dbo.Outlet", "Id");
            AddForeignKey("dbo.ReservationExclusionDate", "OutletId_Id", "dbo.Outlet", "Id");
            AddForeignKey("dbo.Outlet", "Company_Id", "dbo.Company", "Id");
        }
    }
}
