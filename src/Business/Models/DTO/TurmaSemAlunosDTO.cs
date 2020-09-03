using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.DTO
{
    /// <summary>
    /// Turma
    /// </summary>
    public class TurmaSemAlunosDTO
    {
        /// <summary>
        /// Codigo da Turma
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nome da Turma
        /// </summary>
        public string Nome { get; set; }

        public void TurmaParaTurmaDTO(Turma turma)
        {
            this.Codigo = turma.Codigo;
            this.Nome = turma.Nome;
        }
    }
}
