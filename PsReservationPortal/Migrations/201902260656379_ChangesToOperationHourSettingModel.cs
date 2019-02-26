namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToOperationHourSettingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CloseDateSetting", "Name", c => c.String());
            AddColumn("dbo.OperationHourSetting", "OperationType", c => c.Int(nullable: false));
            AddColumn("dbo.OperationHourSetting", "StartHour", c => c.Int(nullable: false));
            AddColumn("dbo.OperationHourSetting", "EndHour", c => c.Int(nullable: false));
            AddColumn("dbo.OperationHourSetting", "StartMinute", c => c.Int(nullable: false));
            AddColumn("dbo.OperationHourSetting", "EndMinute", c => c.Int(nullable: false));
            DropColumn("dbo.CloseDateSetting", "Remark");
            DropColumn("dbo.OperationHourSetting", "OpenHour");
            DropColumn("dbo.OperationHourSetting", "CloseHour");
            DropColumn("dbo.OperationHourSetting", "StartBreak");
            DropColumn("dbo.OperationHourSetting", "EndBreak");
            DropColumn("dbo.OperationHourSetting", "StartResvPeriod");
            DropColumn("dbo.OperationHourSetting", "EndResvPeriod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OperationHourSetting", "EndResvPeriod", c => c.Double(nullable: false));
            AddColumn("dbo.OperationHourSetting", "StartResvPeriod", c => c.Double(nullable: false));
            AddColumn("dbo.OperationHourSetting", "EndBreak", c => c.Double(nullable: false));
            AddColumn("dbo.OperationHourSetting", "StartBreak", c => c.Double(nullable: false));
            AddColumn("dbo.OperationHourSetting", "CloseHour", c => c.Double(nullable: false));
            AddColumn("dbo.OperationHourSetting", "OpenHour", c => c.Double(nullable: false));
            AddColumn("dbo.CloseDateSetting", "Remark", c => c.String());
            DropColumn("dbo.OperationHourSetting", "EndMinute");
            DropColumn("dbo.OperationHourSetting", "StartMinute");
            DropColumn("dbo.OperationHourSetting", "EndHour");
            DropColumn("dbo.OperationHourSetting", "StartHour");
            DropColumn("dbo.OperationHourSetting", "OperationType");
            DropColumn("dbo.CloseDateSetting", "Name");
        }
    }
}
