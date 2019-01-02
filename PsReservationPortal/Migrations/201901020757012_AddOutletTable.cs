namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOutletTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Outlet",
                c => new
                    {
                        OutletId = c.Long(nullable: false, identity: true),
                        OutletName = c.String(nullable: false),
                        OutletLocation = c.String(),
                        OutletAddress = c.String(),
                        Active = c.Boolean(nullable: false),
                        Company_Id = c.Long(),
                    })
                .PrimaryKey(t => t.OutletId)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Outlet", "Company_Id", "dbo.Company");
            DropIndex("dbo.Outlet", new[] { "Company_Id" });
            DropTable("dbo.Outlet");
        }
    }
}
