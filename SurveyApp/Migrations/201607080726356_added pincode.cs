namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpincode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyClients", "Pincode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyClients", "Pincode");
        }
    }
}
