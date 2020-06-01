namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizartabelaUsuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "usuarioSenha", c => c.String(nullable: false));
            DropColumn("dbo.Usuarios", "usarioSenha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "usarioSenha", c => c.String(nullable: false));
            DropColumn("dbo.Usuarios", "usuarioSenha");
        }
    }
}
