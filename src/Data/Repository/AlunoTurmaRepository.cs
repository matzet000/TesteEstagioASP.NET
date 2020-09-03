using Business.Intefaces;
using Business.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AlunoTurmaRepository : Repository<AlunoTurma>, IAlunoTurmaRepository
    {
        public AlunoTurmaRepository(DataDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Aluno>> ObterAlunosDaTurma(string codigo)
        {
            var alunosturma = await Buscar(a => a.Codigo.Equals(codigo));
            var alunos = new List<Aluno>();

            foreach(AlunoTurma alunoTurma in alunosturma)
            {
               Aluno aluno = Db.Alunos.Where(a => a.Matricula.Equals(alunoTurma.Matricula)).FirstOrDefault();
               alunos.Add(aluno);
            }

            return alunos;
        }

        public async Task<IEnumerable<Turma>> ObterTurmaDoAluno(string matricula)
        {
            var alunosturma = await Buscar(a => a.Matricula.Equals(matricula));
            var turmas = new List<Turma>();

            foreach (AlunoTurma alunoTurma in alunosturma)
            {
                Turma turma = Db.Turmas.Where(a => a.Codigo.Equals(alunoTurma.Codigo)).FirstOrDefault();
                turmas.Add(turma);
            }

            return turmas;
        }
    }
}
