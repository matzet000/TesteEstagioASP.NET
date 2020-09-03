using Business.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace Data.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base("name=TesteDBConnectionString")
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<Usuario>().ToTable("dbo.Usuarios");

            modelBuilder.Entity<Aluno>()
                .HasKey(a => a.Matricula);

            modelBuilder.Entity<Aluno>()
                .Property(a => a.Matricula)
                .HasMaxLength(9);

            modelBuilder.Entity<Aluno>()
                .Property(a => a.Nome)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<Aluno>().ToTable("dbo.Alunos");

            modelBuilder.Entity<Turma>().HasKey(t => t.Codigo);

            modelBuilder.Entity<Turma>()
                .Property(t => t.Codigo)
                .HasMaxLength(9);

            modelBuilder.Entity<Turma>()
                .Property(a => a.Nome)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<Turma>().ToTable("dbo.Turmas");

            modelBuilder.Entity<AlunoTurma>().HasKey(at => new { at.Codigo, at.Matricula });

            modelBuilder.Entity<AlunoTurma>()
                .HasRequired<Aluno>(at => at.Aluno)
                .WithMany(a => a.AlunoTurmas)
                .HasForeignKey(at => at.Matricula)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<AlunoTurma>()
                .HasRequired<Turma>(at => at.Turma)
                .WithMany(a => a.AlunoTurmas)
                .HasForeignKey(at => at.Codigo)
                .WillCascadeOnDelete(false);

        }
    }
}
