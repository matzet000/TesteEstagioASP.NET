using Business.Intefaces;
using Business.Models;
using Business.Models.DTO;
using Business.Models.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public TurmaService(ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository)
        {
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
        }

        public async Task Adicionar(Turma turma)
        {
            if (!ExecutarValidacao(turma))
            {
                throw new ArgumentException("A turma não está valida");
            }

            await _turmaRepository.Adicionar(turma);
        }

        public async Task Atualizar(Turma turma)
        {
            if (!ExecutarValidacao(turma))
            {
                throw new ArgumentException("A turma não está valida");
            }
            
            var turmaSitema = await _turmaRepository.ObterTurmaPorCodigo(turma.Codigo);

            if (turma == null)
            {
                throw new ArgumentException("A turma não está cadastrada no sistema");
            }

            await _turmaRepository.Atualizar(turma);
        }

        public async Task Remover(string codigo)
        {
            var turma = await _turmaRepository.ObterTurmaPorCodigo(codigo);

            if(turma == null)
            {
                throw new ArgumentException("A turma não está cadastrada no sistema");
            }

            var tamanhoDaTurma = await _alunoTurmaRepository.ObterAlunosDaTurma(codigo);

            if(tamanhoDaTurma.Count() > 0)
            {
                throw new ArgumentException("A turma não pode ser exluida no sistema, pois há aluno na turma");
            }

            await _turmaRepository.Remover(codigo);
        }
        public async Task<List<TurmaDTO>> ObterTurmasComAlunos()
        {
            var turmas = await _turmaRepository.ObterTodos();
            var turmasDTO = TurmaParaTurmaDTOList(turmas);

            foreach (TurmaDTO turma in turmasDTO)
            {
                turma.Alunos = new List<AlunoSemTurmaDTO>();
                var alunos = await _alunoTurmaRepository.ObterAlunosDaTurma(turma.Codigo);

                foreach (Aluno aluno in alunos)
                {
                    AlunoSemTurmaDTO alunoDTO = new AlunoSemTurmaDTO();
                    alunoDTO.AlunoParaAlunoDTO(aluno);
                    turma.Alunos.Add(alunoDTO);
                }
            }

            return turmasDTO;
        }

        public async Task<TurmaDTO> ObterTurmaComAlunos(string codigo)
        {
            var turma = await _turmaRepository.ObterTurmaPorCodigo(codigo);

            TurmaDTO turmaDTO = new TurmaDTO();
            turmaDTO.TurmaParaTurmaDTO(turma);
            turmaDTO.Alunos = new List<AlunoSemTurmaDTO>();

            var alunos = await _alunoTurmaRepository.ObterAlunosDaTurma(codigo);

            foreach(Aluno aluno in alunos)
            {
                AlunoSemTurmaDTO alunoDTO = new AlunoSemTurmaDTO();
                alunoDTO.AlunoParaAlunoDTO(aluno);
                turmaDTO.Alunos.Add(alunoDTO);
            }

            return turmaDTO;
        }

        public void Dispose()
        {
            _turmaRepository?.Dispose();
        }

        private bool ExecutarValidacao(Turma turma)
        {
            TurmaValidation validator = new TurmaValidation();
            ValidationResult result = validator.Validate(turma);

            if (result.IsValid) return true;

            return false;
        }

        private List<TurmaDTO> TurmaParaTurmaDTOList(List<Turma> turmas)
        {
            List<TurmaDTO> turmasDTO = new List<TurmaDTO>();

            foreach (Turma turma in turmas)
            {
                TurmaDTO turmaDTO = new TurmaDTO();
                turmaDTO.TurmaParaTurmaDTO(turma);
                turmasDTO.Add(turmaDTO);
            }

            return turmasDTO;
        }
    }
}
