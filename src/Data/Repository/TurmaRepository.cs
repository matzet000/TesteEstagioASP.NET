using Business.Intefaces;
using Business.Models;
using Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(DataDbContext db) : base(db)
        {
        }

        public async Task<Turma> ObterTurmaPorCodigo(string codigo)
        {
            return await DbSet.FindAsync(codigo);
        }

        public async Task Remover(string codigo)
        {
            var turmaDelete = DbSet.Where(t => t.Codigo.Equals(codigo)).FirstOrDefault();
            DbSet.Remove(turmaDelete);
            await SaveChanges();
        }
    }
}
