namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedanalystIdtoclient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyClients", "AnalystId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyClients", "AnalystId");
        }
    }
}
