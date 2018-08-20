namespace WpfApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criacaotabelausuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_USUARIO",
                c => new
                    {
                        ID_USUARIO = c.Int(nullable: false, identity: true),
                        DS_MATRICULA = c.String(maxLength: 7),
                        DS_SENHA = c.String(maxLength: 250),
                        DT_CRIACAO = c.DateTime(nullable: false),
                        DS_NULO = c.Int(),
                        DS_NAO_NULO = c.Int(nullable: false),
                        DS_MINIMO = c.String(),
                    })
                .PrimaryKey(t => t.ID_USUARIO)
                .Index(t => t.DS_MATRICULA, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TB_USUARIO", new[] { "DS_MATRICULA" });
            DropTable("dbo.TB_USUARIO");
        }
    }
}
