namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentalModelFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rentals", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Rentals", name: "MovieId", newName: "Movie_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameIndex(table: "dbo.Rentals", name: "IX_MovieId", newName: "IX_Movie_Id");
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime(nullable: false));
            RenameIndex(table: "dbo.Rentals", name: "IX_Movie_Id", newName: "IX_MovieId");
            RenameIndex(table: "dbo.Rentals", name: "IX_Customer_Id", newName: "IX_CustomerId");
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "MovieId");
            RenameColumn(table: "dbo.Rentals", name: "Customer_Id", newName: "CustomerId");
        }
    }
}
