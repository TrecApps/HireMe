namespace HireMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredKnowledge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "RequiredKnowledge", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "RequiredKnowledge");
        }
    }
}
