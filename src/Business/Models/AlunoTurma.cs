using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class AlunoTurma
    {
        public string Matricula { get; set; }
        public Aluno Aluno { get; set; }

        public string Codigo { get; set; }
        public Turma Turma { get; set; }
    }
}
