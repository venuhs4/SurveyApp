namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSortingIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyDetails", "SortingIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyDetails", "SortingIndex");
        }
    }
}
