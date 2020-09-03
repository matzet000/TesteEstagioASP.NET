﻿using Business.Intefaces;
using Business.Models;
using Data.Context;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(DataDbContext db) : base(db)
        {
        }

        public async Task<Aluno> ObterAlunoPorMatricula(string matricula)
        {
            return await DbSet.FindAsync(matricula);
        }

        public async Task Remover(string matricula)
        {
            DbSet.Remove(new Aluno { Matricula = matricula });
            await SaveChanges();
        }
    }
}
