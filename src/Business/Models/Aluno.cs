using System.Collections;
using System.Collections.Generic;

namespace Business.Models
{

    public class Aluno
    {
        /* 
         * Fromato: ano, semetre, 4 numeros aleatorios
         */
        public string Matricula { get; set; }
        public string Nome { get; set; }

        public IList<AlunoTurma> AlunoTurmas { get; set; }
    }
}
