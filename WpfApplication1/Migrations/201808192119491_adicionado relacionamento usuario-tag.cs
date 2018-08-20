namespace WpfApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionadorelacionamentousuariotag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_TAG", "ID_USUARIO", c => c.Int(nullable: false));
            CreateIndex("dbo.TB_TAG", "ID_USUARIO");
            AddForeignKey("dbo.TB_TAG", "ID_USUARIO", "dbo.TB_USUARIO", "ID_USUARIO", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_TAG", "ID_USUARIO", "dbo.TB_USUARIO");
            DropIndex("dbo.TB_TAG", new[] { "ID_USUARIO" });
            DropColumn("dbo.TB_TAG", "ID_USUARIO");
        }
    }
}
