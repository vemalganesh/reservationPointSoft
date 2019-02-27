namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReservationOrderModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CloseDateSetting", newName: "ReservationExclusionDate");
            RenameTable(name: "dbo.ReservationDaySetting", newName: "ReservationExclusionDay");
            CreateTable(
                "dbo.ReservationOrder",
                c => new
                    {
                        ReservationNum = c.Long(nullable: false, identity: true),
                        DinerName = c.String(),
                        PhoneNum = c.String(maxLength: 15),
                        ReserveDateTime = c.DateTime(nullable: false),
                        ReservationRequest = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeUpdated = c.DateTime(nullable: false),
                        DinerModel_Id = c.Long(),
                        TableId_Id = c.Long(),
                    })
                .PrimaryKey(t => t.ReservationNum)
                .ForeignKey("dbo.Diner", t => t.DinerModel_Id)
                .ForeignKey("dbo.Table", t => t.TableId_Id)
                .Index(t => t.DinerModel_Id)
                .Index(t => t.TableId_Id);
            
            AddColumn("dbo.ReservationExclusionDate", "ExlusionDateName", c => c.String());
            AddColumn("dbo.ReservationExclusionDay", "Monday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Tuesday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Wednesday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Thursday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Friday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Saturday", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "Sunday", c => c.Int(nullable: false));
            DropColumn("dbo.ReservationExclusionDate", "Remark");
            DropColumn("dbo.ReservationExclusionDay", "MonOff");
            DropColumn("dbo.ReservationExclusionDay", "TueOff");
            DropColumn("dbo.ReservationExclusionDay", "WedOff");
            DropColumn("dbo.ReservationExclusionDay", "ThuOff");
            DropColumn("dbo.ReservationExclusionDay", "FriOff");
            DropColumn("dbo.ReservationExclusionDay", "SatOff");
            DropColumn("dbo.ReservationExclusionDay", "SunOff");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationExclusionDay", "SunOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "SatOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "FriOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "ThuOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "WedOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "TueOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDay", "MonOff", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReservationExclusionDate", "Remark", c => c.String());
            DropForeignKey("dbo.ReservationOrder", "TableId_Id", "dbo.Table");
            DropForeignKey("dbo.ReservationOrder", "DinerModel_Id", "dbo.Diner");
            DropIndex("dbo.ReservationOrder", new[] { "TableId_Id" });
            DropIndex("dbo.ReservationOrder", new[] { "DinerModel_Id" });
            DropColumn("dbo.ReservationExclusionDay", "Sunday");
            DropColumn("dbo.ReservationExclusionDay", "Saturday");
            DropColumn("dbo.ReservationExclusionDay", "Friday");
            DropColumn("dbo.ReservationExclusionDay", "Thursday");
            DropColumn("dbo.ReservationExclusionDay", "Wednesday");
            DropColumn("dbo.ReservationExclusionDay", "Tuesday");
            DropColumn("dbo.ReservationExclusionDay", "Monday");
            DropColumn("dbo.ReservationExclusionDate", "ExlusionDateName");
            DropTable("dbo.ReservationOrder");
            RenameTable(name: "dbo.ReservationExclusionDay", newName: "ReservationDaySetting");
            RenameTable(name: "dbo.ReservationExclusionDate", newName: "CloseDateSetting");
        }
    }
}
