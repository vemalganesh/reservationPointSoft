namespace PsReservationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c471f91b-754b-4cf2-aa73-c304492d4562', N'Guest')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4a5e1d13-275a-4919-9823-14c772358ee2', N'Users')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9dd62907-d184-496e-8d7b-97a4cfddfd3e', N'Manager')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'863f6e39-1a10-44bb-b18e-6181d347bf5f', N'CompanyAdmin')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3ffa3347-7166-43d6-a976-5c3d1c377eef', N'PointsoftSupport')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'855e2adf-4897-4ffa-96fa-59663b205a51', N'SuperAdmin')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
