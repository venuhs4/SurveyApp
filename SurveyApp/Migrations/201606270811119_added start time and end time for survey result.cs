namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstarttimeandendtimeforsurveyresult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyResults", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.SurveyResults", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SurveyResults", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SurveyResults", "UserId", c => c.Long(nullable: false));
            DropColumn("dbo.SurveyResults", "EndTime");
            DropColumn("dbo.SurveyResults", "StartTime");
        }
    }
}
