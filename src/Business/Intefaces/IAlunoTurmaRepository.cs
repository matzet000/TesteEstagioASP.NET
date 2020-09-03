using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface IAlunoTurmaRepository : IRepository<AlunoTurma>
    {
        Task<IEnumerable<Aluno>> ObterAlunosDaTurma(string codigo);
        Task<IEnumerable<Turma>> ObterTurmaDoAluno(string matricula);

        Task RemoverAlunosDaTurma(string codigo);
        Task RemoverTurmasDoAluno(string matricula);
    }
}
