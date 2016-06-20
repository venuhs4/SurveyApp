namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedinitialtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyDetails",
                c => new
                    {
                        SurveyDetailId = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        Dependent = c.Boolean(nullable: false),
                        DependentItemID = c.Long(nullable: false),
                        Dependancy = c.String(),
                        Prompt = c.String(),
                        DelimitedItemList = c.String(),
                    })
                .PrimaryKey(t => t.SurveyDetailId);
            
            CreateTable(
                "dbo.SurveyGroups",
                c => new
                    {
                        SurveyGroupId = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyGroupId);
            
            CreateTable(
                "dbo.SurveyResults",
                c => new
                    {
                        SurveyResultId = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyResultId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyResults");
            DropTable("dbo.SurveyGroups");
            DropTable("dbo.SurveyDetails");
        }
    }
}
