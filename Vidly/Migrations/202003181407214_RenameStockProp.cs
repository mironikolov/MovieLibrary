namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameStockProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Stock", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
