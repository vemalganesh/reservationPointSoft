namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOperationTypeModelStructure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperationType", "OutletId", c => c.Long(nullable: false));
            AddColumn("dbo.OperationType", "DateTimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.OperationType", "DateTimeUpdated", c => c.DateTime(nullable: false));
            CreateIndex("dbo.OperationType", "OutletId");
            AddForeignKey("dbo.OperationType", "OutletId", "dbo.Outlet", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OperationType", "OutletId", "dbo.Outlet");
            DropIndex("dbo.OperationType", new[] { "OutletId" });
            DropColumn("dbo.OperationType", "DateTimeUpdated");
            DropColumn("dbo.OperationType", "DateTimeCreated");
            DropColumn("dbo.OperationType", "OutletId");
        }
    }
}
