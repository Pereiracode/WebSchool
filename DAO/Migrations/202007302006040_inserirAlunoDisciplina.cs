namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inserirAlunoDisciplina : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlunoDisciplinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataMatricula = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                        DisciplinaId = c.Int(nullable: false),
                        NrMatricula = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alunos", t => t.NrMatricula, cascadeDelete: true)
                .ForeignKey("dbo.Disciplinas", t => t.DisciplinaId, cascadeDelete: true)
                .Index(t => t.DisciplinaId)
                .Index(t => t.NrMatricula);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlunoDisciplinas", "DisciplinaId", "dbo.Disciplinas");
            DropForeignKey("dbo.AlunoDisciplinas", "NrMatricula", "dbo.Alunos");
            DropIndex("dbo.AlunoDisciplinas", new[] { "NrMatricula" });
            DropIndex("dbo.AlunoDisciplinas", new[] { "DisciplinaId" });
            DropTable("dbo.AlunoDisciplinas");
        }
    }
}
