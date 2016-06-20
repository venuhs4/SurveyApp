namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSurveyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        SurveyId = c.Long(nullable: false, identity: true),
                        Duty = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        LinkId = c.String(),
                    })
                .PrimaryKey(t => t.SurveyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Surveys");
        }
    }
}
