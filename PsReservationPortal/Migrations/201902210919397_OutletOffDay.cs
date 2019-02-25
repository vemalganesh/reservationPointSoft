namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OutletOffDay : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutletWorkingDate", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.OutletOffDay", "Company_Id", "dbo.Company");
            DropIndex("dbo.OutletWorkingDate", new[] { "Company_Id" });
            DropIndex("dbo.OutletOffDay", new[] { "Company_Id" });
            DropTable("dbo.OutletWorkingDate");
            DropTable("dbo.OutletOffDay");
        }
    }
}
