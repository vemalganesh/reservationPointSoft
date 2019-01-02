namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserExtraInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserExtraInfo",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Activated = c.Boolean(nullable: false),
                        Suspended = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserExtraInfo");
        }
    }
}
