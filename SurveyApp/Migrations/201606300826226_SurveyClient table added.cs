namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyClienttableadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalystSurveys",
                c => new
                    {
                        AnalystSurveyId = c.Long(nullable: false, identity: true),
                        ClientId = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false),
                        AnalystSurveyName = c.String(),
                        AnalystSurveyBrief = c.String(),
                        Description = c.String(),
                        Response = c.String(),
                        InternalNotes = c.String(),
                    })
                .PrimaryKey(t => t.AnalystSurveyId);
            
            CreateTable(
                "dbo.SurveyClients",
                c => new
                    {
                        SurveyClientId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordMd5 = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Company = c.String(),
                        SurveyResultId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyClientId)
                .ForeignKey("dbo.SurveyResults", t => t.SurveyResultId, cascadeDelete: true)
                .Index(t => t.SurveyResultId);
            
            DropColumn("dbo.SurveyResults", "FirstName");
            DropColumn("dbo.SurveyResults", "LastName");
            DropColumn("dbo.SurveyResults", "UserId");
            DropColumn("dbo.SurveyAnswers", "RegisteredUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyAnswers", "RegisteredUser", c => c.Boolean(nullable: false));
            AddColumn("dbo.SurveyResults", "UserId", c => c.String());
            AddColumn("dbo.SurveyResults", "LastName", c => c.String());
            AddColumn("dbo.SurveyResults", "FirstName", c => c.String());
            DropForeignKey("dbo.SurveyClients", "SurveyResultId", "dbo.SurveyResults");
            DropIndex("dbo.SurveyClients", new[] { "SurveyResultId" });
            DropTable("dbo.SurveyClients");
            DropTable("dbo.AnalystSurveys");
        }
    }
}
