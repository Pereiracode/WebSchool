﻿using System.Data.Entity;
using Model.Models;

namespace DAO.Dao
{
    public class EscolaWebContext : DbContext
    {
        public EscolaWebContext() : base("strconmysql")
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        public System.Data.Entity.DbSet<Model.Models.AlunoDisciplina> AlunoDisciplinas { get; set; }
    }
}
