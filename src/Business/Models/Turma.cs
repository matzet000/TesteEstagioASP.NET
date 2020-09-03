using System.Collections.Generic;

namespace Business.Models
{
    /// <summary>
    /// Turma
    /// </summary>
    public class Turma
    {
        /// <summary>
        /// Codigo da turma
        /// </summary>
        public string Codigo { get; set; }
        
        /// <summary>
        /// Nome da turma
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de alunos
        /// </summary>
        public IList<AlunoTurma> AlunoTurmas { get; set; }
    }
}
