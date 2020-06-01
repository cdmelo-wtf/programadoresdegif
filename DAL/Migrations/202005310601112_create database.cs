namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        grupoId = c.Int(nullable: false, identity: true),
                        grupoNome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.grupoId);
            
            CreateTable(
                "dbo.Sistemas",
                c => new
                    {
                        sistemaId = c.Int(nullable: false, identity: true),
                        sistemaNome = c.String(nullable: false),
                        sistemaCaminho = c.String(nullable: false),
                        sistemaExecutavel = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.sistemaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        usuarioId = c.Int(nullable: false, identity: true),
                        usuarioLogin = c.String(nullable: false),
                        usarioSenha = c.String(nullable: false),
                        usuarioGrupo_grupoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Grupos", t => t.usuarioGrupo_grupoId, cascadeDelete: true)
                .Index(t => t.usuarioGrupo_grupoId);
            
            CreateTable(
                "dbo.SistemaDTOGrupoDTOes",
                c => new
                    {
                        SistemaDTO_sistemaId = c.Int(nullable: false),
                        GrupoDTO_grupoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SistemaDTO_sistemaId, t.GrupoDTO_grupoId })
                .ForeignKey("dbo.Sistemas", t => t.SistemaDTO_sistemaId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoDTO_grupoId, cascadeDelete: true)
                .Index(t => t.SistemaDTO_sistemaId)
                .Index(t => t.GrupoDTO_grupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "usuarioGrupo_grupoId", "dbo.Grupos");
            DropForeignKey("dbo.SistemaDTOGrupoDTOes", "GrupoDTO_grupoId", "dbo.Grupos");
            DropForeignKey("dbo.SistemaDTOGrupoDTOes", "SistemaDTO_sistemaId", "dbo.Sistemas");
            DropIndex("dbo.SistemaDTOGrupoDTOes", new[] { "GrupoDTO_grupoId" });
            DropIndex("dbo.SistemaDTOGrupoDTOes", new[] { "SistemaDTO_sistemaId" });
            DropIndex("dbo.Usuarios", new[] { "usuarioGrupo_grupoId" });
            DropTable("dbo.SistemaDTOGrupoDTOes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Sistemas");
            DropTable("dbo.Grupos");
        }
    }
}
