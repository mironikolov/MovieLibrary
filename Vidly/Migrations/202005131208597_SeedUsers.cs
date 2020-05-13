namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'58b460ea-b5a0-4db1-8fa4-b2b1d2d230de', N'guest@vidly.com', 0, N'AF6jglMqOISE20a8zpHC7zYJ32hWVwxMP9VXzIBVuhPuVgG26F/t7s36/1AR3FAsqA==', N'705dfcf0-5efb-4c14-830a-e0a773bafc7a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'96d2fe96-bd7b-4a9f-bce5-11016e50ee55', N'admin@vidly.com', 0, N'AMjF3wMnJa3KhAhkBNXPPr1/B2AOUihEDT4+3zdU9iC6wzTVoLCaJCtwaegtm0IaPQ==', N'5762166e-3b6d-4de3-ba64-de1f1cd38648', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'93a57be9-d937-4eeb-b211-99b0324666e5', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'96d2fe96-bd7b-4a9f-bce5-11016e50ee55', N'93a57be9-d937-4eeb-b211-99b0324666e5')

");
        }
        
        public override void Down()
        {
        }
    }
}
