namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajusteretornoidgrupodeUsuario : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuarios", "usuarioGrupo_grupoId", "dbo.Grupos");
            DropIndex("dbo.Usuarios", new[] { "usuarioGrupo_grupoId" });
            AddColumn("dbo.Usuarios", "usuarioGrupo", c => c.Int(nullable: false));
            DropColumn("dbo.Usuarios", "usuarioGrupo_grupoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "usuarioGrupo_grupoId", c => c.Int(nullable: false));
            DropColumn("dbo.Usuarios", "usuarioGrupo");
            CreateIndex("dbo.Usuarios", "usuarioGrupo_grupoId");
            AddForeignKey("dbo.Usuarios", "usuarioGrupo_grupoId", "dbo.Grupos", "grupoId", cascadeDelete: true);
        }
    }
}
