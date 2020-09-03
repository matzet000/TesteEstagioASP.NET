using System.Collections;
using System.Collections.Generic;

namespace Business.Models
{
    /// <summary>
    /// Aluno
    /// </summary>
    public class Aluno
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
        public IList<AlunoTurma> AlunoTurmas { get; set; }
    }
}
