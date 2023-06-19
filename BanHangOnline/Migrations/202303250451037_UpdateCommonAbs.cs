namespace BanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommonAbs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Adv", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Category", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_News", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Posts", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Contact", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Order", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Product", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_ProductCategory", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_ProductCategory", "ModifiedDate");
            DropColumn("dbo.tb_Product", "ModifiedDate");
            DropColumn("dbo.tb_Order", "ModifiedDate");
            DropColumn("dbo.tb_Contact", "ModifiedDate");
            DropColumn("dbo.tb_Posts", "ModifiedDate");
            DropColumn("dbo.tb_News", "ModifiedDate");
            DropColumn("dbo.tb_Category", "ModifiedDate");
            DropColumn("dbo.tb_Adv", "ModifiedDate");
        }
    }
}
