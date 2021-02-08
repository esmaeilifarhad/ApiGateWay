namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Url", c => c.String());
            AddColumn("dbo.Logs", "BeforeAfter", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "BeforeAfter");
            DropColumn("dbo.Logs", "Url");
        }
    }
}
