namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        Matricula = c.String(nullable: false, maxLength: 9),
                        Nome = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Matricula);
            
            CreateTable(
                "dbo.AlunoTurmas",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 9),
                        Matricula = c.String(nullable: false, maxLength: 9),
                    })
                .PrimaryKey(t => new { t.Codigo, t.Matricula })
                .ForeignKey("dbo.Alunos", t => t.Matricula)
                .ForeignKey("dbo.Turmas", t => t.Codigo)
                .Index(t => t.Codigo)
                .Index(t => t.Matricula);
            
            CreateTable(
                "dbo.Turmas",
                c => new
                    {
                        Codigo = c.String(nullable: false, maxLength: 9),
                        Nome = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlunoTurmas", "Codigo", "dbo.Turmas");
            DropForeignKey("dbo.AlunoTurmas", "Matricula", "dbo.Alunos");
            DropIndex("dbo.AlunoTurmas", new[] { "Matricula" });
            DropIndex("dbo.AlunoTurmas", new[] { "Codigo" });
            DropTable("dbo.Turmas");
            DropTable("dbo.AlunoTurmas");
            DropTable("dbo.Alunos");
        }
    }
}
