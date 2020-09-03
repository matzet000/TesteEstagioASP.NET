using Business.Intefaces;
using Business.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataDbContext db) : base(db)
        {
        }

        public async Task Remover(Guid id)
        {
            var usuarioDelete = DbSet.Where(u => u.Id == id).FirstOrDefault();
            DbSet.Remove(usuarioDelete);
            await SaveChanges();
        }
    }
}
