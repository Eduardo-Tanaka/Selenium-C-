namespace WpfApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criadoclassetag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_TAG",
                c => new
                    {
                        ID_TAG = c.Int(nullable: false, identity: true),
                        NM_TAG = c.String(),
                        DS_TAG = c.String(),
                        DT_TAG = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_TAG);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TB_TAG");
        }
    }
}
