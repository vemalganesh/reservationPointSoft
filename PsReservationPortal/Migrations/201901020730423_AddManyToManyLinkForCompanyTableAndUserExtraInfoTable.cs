namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyLinkForCompanyTableAndUserExtraInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserExtraInfoModelCompanyModels",
                c => new
                    {
                        UserExtraInfoModel_UserId = c.String(nullable: false, maxLength: 128),
                        CompanyModel_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserExtraInfoModel_UserId, t.CompanyModel_Id })
                .ForeignKey("dbo.UserExtraInfo", t => t.UserExtraInfoModel_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.CompanyModel_Id, cascadeDelete: true)
                .Index(t => t.UserExtraInfoModel_UserId)
                .Index(t => t.CompanyModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "CompanyModel_Id", "dbo.Company");
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", "dbo.UserExtraInfo");
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "CompanyModel_Id" });
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_UserId" });
            DropTable("dbo.UserExtraInfoModelCompanyModels");
        }
    }
}
