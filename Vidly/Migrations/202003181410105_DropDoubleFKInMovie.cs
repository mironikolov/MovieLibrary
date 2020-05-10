namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropDoubleFKInMovie : DbMigration
    {
        public override void Up()
        {
            //todo: probably fucked up
            //Sql("ALTER TABLE Movies DROP CONSTRAINT DF__Movies__GenreId__01142BA1, COLUMN GenreId");
            Sql("ALTER TABLE Movies ADD GenreId int ");
        }
        
        public override void Down()
        {
            //Sql("ALTER TABLE Movies DROP COLUMN GenreId");
            //Sql("ALTER TABLE Movies ADD CONSTRAINT DF__Movies__GenreId__01142BA1, COLUMN GenreId");
        }
    }
}
