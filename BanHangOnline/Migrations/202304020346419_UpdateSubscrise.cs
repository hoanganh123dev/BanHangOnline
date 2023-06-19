namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubscrise : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Subscribe", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Subscribe", "Email", c => c.String());
        }
    }
}
