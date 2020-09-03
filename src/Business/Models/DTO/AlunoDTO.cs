using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.DTO
{
    /// <summary>
    /// Aluno
    /// </summary>
    public class AlunoDTO
    {
        /// <summary>
        /// Matricula do Aluno
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Nome do Aluno
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Turmas do Aluno
        /// </summary>
        public IList<TurmaSemAlunosDTO> Turmas { get; set; }

        public void AlunoParaAlunoDTO(Aluno aluno)
        {
            this.Matricula = aluno.Matricula;
            this.Nome = aluno.Nome;
        }
    }
}
