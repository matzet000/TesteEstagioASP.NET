using Business.Models;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno> ObterAlunoPorMatricula(string matricula);
        Task Remover(string matricula);
    }
}
