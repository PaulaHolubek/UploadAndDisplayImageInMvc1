namespace UploadAndDisplayImageInMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "Image1", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "Image1");
        }
    }
}
