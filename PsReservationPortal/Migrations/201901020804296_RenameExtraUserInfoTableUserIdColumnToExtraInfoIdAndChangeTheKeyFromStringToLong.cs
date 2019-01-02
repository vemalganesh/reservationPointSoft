namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameExtraUserInfoTableUserIdColumnToExtraInfoIdAndChangeTheKeyFromStringToLong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", "dbo.UserExtraInfo");
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_UserId" });
            RenameColumn(table: "dbo.UserExtraInfoModelCompanyModels", name: "UserExtraInfoModel_UserId", newName: "UserExtraInfoModel_ExtraInfoId");
            DropPrimaryKey("dbo.UserExtraInfo");
            DropPrimaryKey("dbo.UserExtraInfoModelCompanyModels");
            AddColumn("dbo.UserExtraInfo", "ExtraInfoId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.UserExtraInfo", "ExtraInfoId");
            AddPrimaryKey("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_ExtraInfoId", "CompanyModel_Id" });
            CreateIndex("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId");
            AddForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", "dbo.UserExtraInfo", "ExtraInfoId", cascadeDelete: true);
            DropColumn("dbo.UserExtraInfo", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserExtraInfo", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", "dbo.UserExtraInfo");
            DropIndex("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_ExtraInfoId" });
            DropPrimaryKey("dbo.UserExtraInfoModelCompanyModels");
            DropPrimaryKey("dbo.UserExtraInfo");
            AlterColumn("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_ExtraInfoId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserExtraInfo", "ExtraInfoId");
            AddPrimaryKey("dbo.UserExtraInfoModelCompanyModels", new[] { "UserExtraInfoModel_UserId", "CompanyModel_Id" });
            AddPrimaryKey("dbo.UserExtraInfo", "UserId");
            RenameColumn(table: "dbo.UserExtraInfoModelCompanyModels", name: "UserExtraInfoModel_ExtraInfoId", newName: "UserExtraInfoModel_UserId");
            CreateIndex("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId");
            AddForeignKey("dbo.UserExtraInfoModelCompanyModels", "UserExtraInfoModel_UserId", "dbo.UserExtraInfo", "UserId", cascadeDelete: true);
        }
    }
}
