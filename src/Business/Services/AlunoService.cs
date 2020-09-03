using Business.Intefaces;
using Business.Models;
using Business.Models.DTO;
using Business.Models.Validations;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public AlunoService(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository)
        {
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public async Task Adicionar(Aluno aluno, ICollection<Turma> turmas)
        {

            if (!ExecutarValidacao(aluno))
            {
                throw new ArgumentException("O Aluno não está com nome valido");
            }

            string matricula = GerarMatricula();
            var testeMatricula = await _alunoRepository.ObterAlunoPorMatricula(matricula);

            if (testeMatricula != null)
            {
                matricula = GerarMatricula();
            }

            aluno.Matricula = matricula;

            if (turmas.Count() == 0)
            {
                throw new ArgumentException("Deve ser passado pelos menos uma turma");
            }

            await _alunoRepository.Adicionar(aluno);

            foreach (Turma turma in turmas)
            {
                var turmaBuscada = await _turmaRepository.ObterTurmaPorCodigo(turma.Codigo);
                if (turmaBuscada == null)
                {
                    await _alunoRepository.Remover(matricula);
                    throw new ArgumentException("O Turma não está cadastrada no sistema");
                }

                var alunosNaTurma = await _alunoTurmaRepository.ObterAlunosDaTurma(turma.Codigo);

                if (alunosNaTurma.Count() >= 5)
                {
                    await _alunoRepository.Remover(matricula);
                    throw new ArgumentException($"A Turma {turma.Codigo} está lotada.");
                }

                var alunoTurma = new AlunoTurma();
                alunoTurma.Matricula = aluno.Matricula;
                alunoTurma.Codigo = turma.Codigo;
                await _alunoTurmaRepository.Adicionar(alunoTurma);
            }
        }

        public async Task Atualizar(Aluno aluno, ICollection<Turma> turmas) 
        {
            if (!ExecutarValidacao(aluno))
            {
                throw new ArgumentException("O Aluno não está com nome valido");
            }

            var alunoExiste = await VerificarAlunoExite(aluno);

            if (!alunoExiste)
            {
                throw new ArgumentException("O Aluno não existe no sistema");
            }

            if(turmas.Count == 0)
            {
                await _alunoTurmaRepository.RemoverTurmasDoAluno(aluno.Matricula);
            }
            else
            {
                foreach(Turma turmaItem in turmas)
                {
                    var turmaBuscada = await _turmaRepository.ObterTurmaPorCodigo(turmaItem.Codigo);

                    if (turmaBuscada == null)
                    {
                        throw new ArgumentException("O Turma não está cadastrada no sistema");
                    }
                }

                var turmasAntigas = await _alunoTurmaRepository.ObterTurmaDoAluno(aluno.Matricula);
                await _alunoTurmaRepository.RemoverTurmasDoAluno(aluno.Matricula);

                foreach (Turma turmaItem in turmas)
                {

                    var alunosNaTurma = await _alunoTurmaRepository.ObterAlunosDaTurma(turmaItem.Codigo);

                    if (alunosNaTurma.Count() >= 5)
                    {
                        foreach(Turma turmaAntiga in turmasAntigas)
                        {
                            var alunoTurmaAntiga = new AlunoTurma();
                            alunoTurmaAntiga.Matricula = aluno.Matricula;
                            alunoTurmaAntiga.Codigo = turmaItem.Codigo;
                            await _alunoTurmaRepository.Adicionar(alunoTurmaAntiga);
                        }
                        throw new ArgumentException($"A Turma {turmaItem.Codigo} está lotada.");
                    }

                    var alunoTurma = new AlunoTurma();
                    alunoTurma.Matricula = aluno.Matricula;
                    alunoTurma.Codigo = turmaItem.Codigo;
                    await _alunoTurmaRepository.Adicionar(alunoTurma);
                }
            }

            var alunoCadastrado = await _alunoRepository.ObterAlunoPorMatricula(aluno.Matricula);

            if (!aluno.Nome.Equals(alunoCadastrado.Nome))
            {
                await _alunoRepository.Atualizar(aluno);
            }
        }

        public async Task Remover(string matricula)
        {
            var aluno = await _alunoRepository.ObterAlunoPorMatricula(matricula);

            if(aluno == null)
            {
                throw new ArgumentException("O Aluno não está cadastrado no sistema");
            }

            var turmas = await _alunoTurmaRepository.ObterTurmaDoAluno(matricula);

            if (turmas.Count() > 0)
            {
                throw new ArgumentException("Aluno matriculado em pelos menos uma turma");
            }
          
            await _alunoRepository.Remover(matricula);
        }
        public async Task<List<AlunoDTO>> ObterAlunosComTurmas()
        {
            var alunos = await _alunoRepository.ObterTodos();
            var alunosDTO = AlunosParaAlunoDTOList(alunos);

            foreach (AlunoDTO aluno in alunosDTO)
            {
                aluno.Turmas = new List<TurmaSemAlunosDTO>();
                var turmas = await _alunoTurmaRepository.ObterTurmaDoAluno(aluno.Matricula);

                foreach(Turma turma in turmas)
                {
                    TurmaSemAlunosDTO turmaDTO = new TurmaSemAlunosDTO();
                    turmaDTO.TurmaParaTurmaDTO(turma);
                    aluno.Turmas.Add(turmaDTO);
                }
            }

            return alunosDTO;
        }

        public async Task<AlunoDTO> ObterAlunoComTurmas(string matricula)
        {
            var aluno = await _alunoRepository.ObterAlunoPorMatricula(matricula);
            
            AlunoDTO alunoDTO = new AlunoDTO();
            alunoDTO.AlunoParaAlunoDTO(aluno);
            alunoDTO.Turmas = new List<TurmaSemAlunosDTO>();

            var turmas = await _alunoTurmaRepository.ObterTurmaDoAluno(alunoDTO.Matricula);
            
            foreach (Turma turma in turmas)
            {
                TurmaSemAlunosDTO turmaDTO = new TurmaSemAlunosDTO();
                turmaDTO.TurmaParaTurmaDTO(turma);
                alunoDTO.Turmas.Add(turmaDTO);
            }

            return alunoDTO;
        }

        public void Dispose()
        {
            _turmaRepository?.Dispose();
            _alunoRepository?.Dispose();
        }
        private async Task<bool> VerificarAlunoExite(Aluno aluno)
        {
            var alunoCadastrado = await _alunoRepository.ObterAlunoPorMatricula(aluno.Matricula);

            if (alunoCadastrado == null)
            {
                return false;
            }

            return true;
        }

        private string GerarMatricula()
        {
            DateTime data = DateTime.Now;
            int ano = data.Year;
            int mes = data.Month;
            int semestre = (mes / 6) > 1 ? 2 : 1;
            Random rnd = new Random();
            var matricula = "" + ano + "" + semestre + "" + rnd.Next(1000, 9999);
            return matricula;
        }

        private bool ExecutarValidacao(Aluno aluno)
        {
            AlunoValidation validator = new AlunoValidation();
            ValidationResult result = validator.Validate(aluno);

            if (result.IsValid) return true;

            return false;
        }

        private List<AlunoDTO> AlunosParaAlunoDTOList(List<Aluno> alunos)
        {
            List<AlunoDTO> alunosDTO = new List<AlunoDTO>();
            
            foreach(Aluno aluno in alunos)
            {
                AlunoDTO alunoDTO = new AlunoDTO();
                alunoDTO.AlunoParaAlunoDTO(aluno);
                alunosDTO.Add(alunoDTO);
            }

            return alunosDTO;
        }
    }
}
