namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiKeyHandler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetApiKey",
                c => new
                    {
                        ApiKey = c.String(nullable: false, maxLength: 128),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApiKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetApiKey");
        }
    }
}
