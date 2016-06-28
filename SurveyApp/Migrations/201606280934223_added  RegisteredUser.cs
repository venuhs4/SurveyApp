namespace SurveyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRegisteredUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyAnswers", "RegisteredUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveyAnswers", "RegisteredUser");
        }
    }
}
