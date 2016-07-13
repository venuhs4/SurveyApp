namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeddescriptionfromAnaystsurvey : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AnalystSurveys", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnalystSurveys", "Description", c => c.String());
        }
    }
}
