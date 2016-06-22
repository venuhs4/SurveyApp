namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsurveygroupmappingtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyGroups", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.SurveyGroups", new[] { "SurveyId" });
            CreateTable(
                "dbo.SurveyGroupMaps",
                c => new
                    {
                        SurveyGroupMapId = c.Long(nullable: false, identity: true),
                        SurveyId = c.Long(nullable: false),
                        SurveyGroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SurveyGroupMapId)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyGroups", t => t.SurveyGroupId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.SurveyGroupId);
            
            DropColumn("dbo.SurveyGroups", "SurveyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyGroups", "SurveyId", c => c.Long(nullable: false));
            DropForeignKey("dbo.SurveyGroupMaps", "SurveyGroupId", "dbo.SurveyGroups");
            DropForeignKey("dbo.SurveyGroupMaps", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.SurveyGroupMaps", new[] { "SurveyGroupId" });
            DropIndex("dbo.SurveyGroupMaps", new[] { "SurveyId" });
            DropTable("dbo.SurveyGroupMaps");
            CreateIndex("dbo.SurveyGroups", "SurveyId");
            AddForeignKey("dbo.SurveyGroups", "SurveyId", "dbo.Surveys", "SurveyId", cascadeDelete: true);
        }
    }
}
