namespace WpfApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criadoclasseTageadicionadodataacessoaclasseusuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_USUARIO", "DT_ACESSO", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_USUARIO", "DT_ACESSO");
        }
    }
}
