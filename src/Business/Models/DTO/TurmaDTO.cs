using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.DTO
{
    public class TurmaDTO
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }

        public IList<AlunoSemturmaDTO> Alunos { get; set; }

        public void TurmaParaTurmaDTO(Turma turma)
        {
            this.Codigo = turma.Codigo;
            this.Nome = turma.Nome;
        }
    }
}
