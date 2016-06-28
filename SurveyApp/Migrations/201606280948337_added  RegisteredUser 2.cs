namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRegisteredUser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyAnswers", "SurveyResultId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyAnswers", "SurveyResultId");
        }
    }
}
