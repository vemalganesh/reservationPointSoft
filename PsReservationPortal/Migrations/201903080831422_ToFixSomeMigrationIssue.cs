namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToFixSomeMigrationIssue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OperationType", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.OperationType", "Updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OperationType", "Updated");
            DropColumn("dbo.OperationType", "Created");
        }
    }
}
