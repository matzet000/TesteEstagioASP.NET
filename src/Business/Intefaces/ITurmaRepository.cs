using Business.Models;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task<Turma> ObterTurmaPorCodigo(string codigo);
        Task Remover(string codigo);
    }
}
