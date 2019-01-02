namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToremovePriorExtraUserInfoChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", "dbo.UserExtraInfo");
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_ExtraInfoId" });
            RenameColumn(table: "dbo.UserExtraInfoModelCompanyModels", name: "UserExtraInfoModel_ExtraInfoId", newName: "UserExtraInfoModel_UserId");
            DropPrimaryKey("dbo.UserExtraInfo");
            DropPrimaryKey("dbo.UserExtraInfoModelCompanyModels");
            AddColumn("dbo.UserExtraInfo", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserExtraInfo", "UserId");
            AddPrimaryKey("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_UserId", "CompanyModel_Id" });
            CreateIndex("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId");
            AddForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", "dbo.UserExtraInfo", "UserId", cascadeDelete: true);
            DropColumn("dbo.UserExtraInfo", "ExtraInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserExtraInfo", "ExtraInfoId", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", "dbo.UserExtraInfo");
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_UserId" });
            DropPrimaryKey("dbo.UserExtraInfoModelCompanyModels");
            DropPrimaryKey("dbo.UserExtraInfo");
            AlterColumn("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", c => c.Long(nullable: false));
            DropColumn("dbo.UserExtraInfo", "UserId");
            AddPrimaryKey("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_ExtraInfoId", "CompanyModel_Id" });
            AddPrimaryKey("dbo.UserExtraInfo", "ExtraInfoId");
            RenameColumn(table: "dbo.UserExtraInfoModelCompanyModels", name: "UserExtraInfoModel_UserId", newName: "UserExtraInfoModel_ExtraInfoId");
            CreateIndex("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId");
            AddForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", "dbo.UserExtraInfo", "ExtraInfoId", cascadeDelete: true);
        }
    }
}
