namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DinerModel_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diner",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNum = c.String(maxLength: 15),
                        ReserveTime = c.DateTime(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        TableId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table", t => t.TableId_Id)
                .Index(t => t.TableId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diner", "TableId_Id", "dbo.Table");
            DropIndex("dbo.Diner", new[] { "TableId_Id" });
            DropTable("dbo.Diner");
        }
    }
}
