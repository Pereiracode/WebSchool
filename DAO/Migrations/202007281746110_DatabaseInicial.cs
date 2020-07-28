namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunos",
                c => new
                    {
                        NrMatricula = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 120, storeType: "nvarchar"),
                        CPF = c.String(unicode: false),
                        DataNascimento = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NrMatricula);
            
            CreateTable(
                "dbo.Disciplinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 60, storeType: "nvarchar"),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        Senha = c.String(nullable: false, maxLength: 12, storeType: "nvarchar"),
                        Email = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Login);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.Disciplinas");
            DropTable("dbo.Alunos");
        }
    }
}
