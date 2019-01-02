namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRegistrationInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRegistrationInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        OutletName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRegistrationInfo");
        }
    }
}
