namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registrationchangespk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyClients", "SurveyResult_SurveyResultId", "dbo.SurveyResults");
            DropIndex("dbo.SurveyClients", new[] { "SurveyResult_SurveyResultId" });
            AddColumn("dbo.SurveyResults", "SurveyClientId", c => c.Long(nullable: false));
            CreateIndex("dbo.SurveyResults", "SurveyClientId");
            AddForeignKey("dbo.SurveyResults", "SurveyClientId", "dbo.SurveyClients", "SurveyClientId", cascadeDelete: true);
            DropColumn("dbo.SurveyClients", "SurveyResult_SurveyResultId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyClients", "SurveyResult_SurveyResultId", c => c.Long());
            DropForeignKey("dbo.SurveyResults", "SurveyClientId", "dbo.SurveyClients");
            DropIndex("dbo.SurveyResults", new[] { "SurveyClientId" });
            DropColumn("dbo.SurveyResults", "SurveyClientId");
            CreateIndex("dbo.SurveyClients", "SurveyResult_SurveyResultId");
            AddForeignKey("dbo.SurveyClients", "SurveyResult_SurveyResultId", "dbo.SurveyResults", "SurveyResultId");
        }
    }
}
