using System.Collections.Generic;

namespace Business.Models
{
    public class Turma
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public IList<AlunoTurma> AlunoTurmas { get; set; }
    }
}
