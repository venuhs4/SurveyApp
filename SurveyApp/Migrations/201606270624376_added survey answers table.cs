namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsurveyanswerstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyAnswers",
                c => new
                    {
                        SurveyAnswerId = c.Long(nullable: false, identity: true),
                        SurveyDetailId = c.Long(nullable: false),
                        SurveyDetailAnswer = c.String(),
                    })
                .PrimaryKey(t => t.SurveyAnswerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyAnswers");
        }
    }
}
