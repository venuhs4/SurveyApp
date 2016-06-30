namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAnalystSurveyType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalystSurveyTypes",
                c => new
                    {
                        AnalystSurveyTypeId = c.Long(nullable: false, identity: true),
                        AnalystTypeName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AnalystSurveyTypeId);
            
            AddColumn("dbo.AnalystSurveys", "AnalystSurveyTypeId", c => c.Long(nullable: false));
            AddColumn("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", c => c.Long());
            CreateIndex("dbo.AnalystSurveys", "AnalystSurveyTypeId");
            CreateIndex("dbo.AnalystSurveys", "SurveyClient_SurveyClientId");
            AddForeignKey("dbo.AnalystSurveys", "AnalystSurveyTypeId", "dbo.AnalystSurveyTypes", "AnalystSurveyTypeId", cascadeDelete: true);
            AddForeignKey("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", "dbo.SurveyClients", "SurveyClientId");
            DropColumn("dbo.AnalystSurveys", "AnalystSurveyName");
            DropColumn("dbo.AnalystSurveys", "AnalystSurveyBrief");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnalystSurveys", "AnalystSurveyBrief", c => c.String());
            AddColumn("dbo.AnalystSurveys", "AnalystSurveyName", c => c.String());
            DropForeignKey("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", "dbo.SurveyClients");
            DropForeignKey("dbo.AnalystSurveys", "AnalystSurveyTypeId", "dbo.AnalystSurveyTypes");
            DropIndex("dbo.AnalystSurveys", new[] { "SurveyClient_SurveyClientId" });
            DropIndex("dbo.AnalystSurveys", new[] { "AnalystSurveyTypeId" });
            DropColumn("dbo.AnalystSurveys", "SurveyClient_SurveyClientId");
            DropColumn("dbo.AnalystSurveys", "AnalystSurveyTypeId");
            DropTable("dbo.AnalystSurveyTypes");
        }
    }
}
