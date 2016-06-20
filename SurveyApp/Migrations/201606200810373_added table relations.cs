namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtablerelations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Surveys", "Duty", c => c.String(maxLength: 50));
            AlterColumn("dbo.Surveys", "Title", c => c.String(maxLength: 50));
            CreateIndex("dbo.SurveyDetails", "SurveyId");
            CreateIndex("dbo.SurveyGroups", "SurveyId");
            CreateIndex("dbo.SurveyResults", "SurveyId");
            AddForeignKey("dbo.SurveyDetails", "SurveyId", "dbo.Surveys", "SurveyId", cascadeDelete: true);
            AddForeignKey("dbo.SurveyGroups", "SurveyId", "dbo.Surveys", "SurveyId", cascadeDelete: true);
            AddForeignKey("dbo.SurveyResults", "SurveyId", "dbo.Surveys", "SurveyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyResults", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.SurveyGroups", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.SurveyDetails", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.SurveyResults", new[] { "SurveyId" });
            DropIndex("dbo.SurveyGroups", new[] { "SurveyId" });
            DropIndex("dbo.SurveyDetails", new[] { "SurveyId" });
            AlterColumn("dbo.Surveys", "Title", c => c.String());
            AlterColumn("dbo.Surveys", "Duty", c => c.String());
        }
    }
}
