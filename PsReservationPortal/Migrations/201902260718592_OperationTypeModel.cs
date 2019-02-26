namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OperationTypeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                        isAllowReserve = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OperationHourSetting", "OperationTypeId_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.OperationHourSetting", "OperationTypeId_Id");
            AddForeignKey("dbo.OperationHourSetting", "OperationTypeId_Id", "dbo.OperationType", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationHourSetting", "OperationTypeId_Id", "dbo.OperationType");
            DropIndex("dbo.OperationHourSetting", new[] { "OperationTypeId_Id" });
            DropColumn("dbo.OperationHourSetting", "OperationTypeId_Id");
            DropTable("dbo.OperationType");
        }
    }
}
