namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSurveynameinsurveygroup2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyGroups", "SurveyGroupName", c => c.String());
            DropColumn("dbo.SurveyGroups", "SurveyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyGroups", "SurveyName", c => c.String());
            DropColumn("dbo.SurveyGroups", "SurveyGroupName");
        }
    }
}
