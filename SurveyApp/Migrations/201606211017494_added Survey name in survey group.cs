namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSurveynameinsurveygroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyGroups", "SurveyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyGroups", "SurveyName");
        }
    }
}
