namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationSetting_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ReservationDuration = c.Double(nullable: false),
                        ReservationAllowBefore = c.Double(nullable: false),
                        CompanyId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId_Id)
                .Index(t => t.CompanyId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSetting", "CompanyId_Id", "dbo.Company");
            DropIndex("dbo.ReservationSetting", new[] { "CompanyId_Id" });
            DropTable("dbo.ReservationSetting");
        }
    }
}
