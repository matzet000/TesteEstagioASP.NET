using Business.Models;
using Business.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface IAlunoService : IDisposable
    {
        Task Adicionar(Aluno aluno, ICollection<Turma> turmas);
        Task Atualizar(Aluno aluno, ICollection<Turma> turmas);
        Task Remover(string matricula);
        Task<List<AlunoDTO>> ObterAlunosComTurmas();
        Task<AlunoDTO> ObterAlunoComTurmas(string matricula);

    }
}
