using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.DTO
{
    public class AlunoDTO
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }

        public IList<TurmaSemAlunosDTO> Turmas { get; set; }

        public void AlunoParaAlunoDTO(Aluno aluno)
        {
            this.Matricula = aluno.Matricula;
            this.Nome = aluno.Nome;
        }
    }
}
