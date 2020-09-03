using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task Remover(Guid id);
    }
}
