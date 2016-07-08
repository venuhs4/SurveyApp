namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registrationchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", "dbo.SurveyClients");
            DropForeignKey("dbo.SurveyClients", "SurveyResultId", "dbo.SurveyResults");
            DropIndex("dbo.AnalystSurveys", new[] { "SurveyClient_SurveyClientId" });
            DropIndex("dbo.SurveyClients", new[] { "SurveyResultId" });
            RenameColumn(table: "dbo.SurveyClients", name: "SurveyResultId", newName: "SurveyResult_SurveyResultId");
            AddColumn("dbo.SurveyClients", "UserId", c => c.String());
            AddColumn("dbo.SurveyClients", "Address", c => c.String());
            AlterColumn("dbo.SurveyClients", "SurveyResult_SurveyResultId", c => c.Long());
            CreateIndex("dbo.SurveyClients", "SurveyResult_SurveyResultId");
            AddForeignKey("dbo.SurveyClients", "SurveyResult_SurveyResultId", "dbo.SurveyResults", "SurveyResultId");
            DropColumn("dbo.AnalystSurveys", "SurveyClient_SurveyClientId");
            DropColumn("dbo.SurveyClients", "UserName");
            DropColumn("dbo.SurveyClients", "PasswordMd5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyClients", "PasswordMd5", c => c.String());
            AddColumn("dbo.SurveyClients", "UserName", c => c.String());
            AddColumn("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", c => c.Long());
            DropForeignKey("dbo.SurveyClients", "SurveyResult_SurveyResultId", "dbo.SurveyResults");
            DropIndex("dbo.SurveyClients", new[] { "SurveyResult_SurveyResultId" });
            AlterColumn("dbo.SurveyClients", "SurveyResult_SurveyResultId", c => c.Long(nullable: false));
            DropColumn("dbo.SurveyClients", "Address");
            DropColumn("dbo.SurveyClients", "UserId");
            RenameColumn(table: "dbo.SurveyClients", name: "SurveyResult_SurveyResultId", newName: "SurveyResultId");
            CreateIndex("dbo.SurveyClients", "SurveyResultId");
            CreateIndex("dbo.AnalystSurveys", "SurveyClient_SurveyClientId");
            AddForeignKey("dbo.SurveyClients", "SurveyResultId", "dbo.SurveyResults", "SurveyResultId", cascadeDelete: true);
            AddForeignKey("dbo.AnalystSurveys", "SurveyClient_SurveyClientId", "dbo.SurveyClients", "SurveyClientId");
        }
    }
}
