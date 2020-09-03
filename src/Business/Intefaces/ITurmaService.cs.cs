using Business.Models;
using Business.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Intefaces
{
    public interface ITurmaService : IDisposable
    {
        Task Adicionar(Turma turma);
        Task Atualizar(Turma turma);
        Task Remover(string codigo);
        Task<List<TurmaDTO>> ObterTurmasComAlunos();
        Task<TurmaDTO> ObterTurmaComAlunos(string codigo);
    }
}
