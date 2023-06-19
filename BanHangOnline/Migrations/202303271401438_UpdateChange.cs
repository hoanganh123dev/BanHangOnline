namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Posts", "SeoKeyWords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Posts", "SeoKeyWords", c => c.String(maxLength: 200));
        }
    }
}
