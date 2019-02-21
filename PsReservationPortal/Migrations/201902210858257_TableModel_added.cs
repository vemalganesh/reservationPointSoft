namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableModel_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Table",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TableNumber = c.String(),
                        Pax = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        OutletId_OutletId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outlet", t => t.OutletId_OutletId)
                .Index(t => t.OutletId_OutletId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Table", "OutletId_OutletId", "dbo.Outlet");
            DropIndex("dbo.Table", new[] { "OutletId_OutletId" });
            DropTable("dbo.Table");
        }
    }
}
