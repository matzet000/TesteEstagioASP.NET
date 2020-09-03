using Api.Filters;
using Api.ViewModels;
using Business.Intefaces;
using Business.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [ErrorFilter]
    [JwtAuthentication]
    public class AlunoController : ApiController
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoRepository alunoRepository, IAlunoService alunoService)
        {
            _alunoRepository = alunoRepository;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Obtem todos os Alunos
        /// </summary>
        /// <remarks>
        /// Alunos com suas turmas. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param>
        /// </param>
        /// <returns code="200"></returns>
        public async Task<IHttpActionResult> Get()
        {
            var alunos = await _alunoService.ObterAlunosComTurmas();
            return Ok(alunos);
        }

        /// <summary>
        /// Obtem o Aluno
        /// </summary>
        /// <remarks>
        /// Aluno com suas turmas. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="matricula">
        /// A matricula do Aluno
        /// </param>
        /// <returns code="200"></returns>
        [Route("api/aluno/{matricula}")]
        public async Task<IHttpActionResult> Get(string matricula)
        {
            var aluno = await _alunoService.ObterAlunoComTurmas(matricula);
            return Ok(aluno);
        }

        /// <summary>
        /// Criar o Aluno com suas turmas
        /// </summary>
        /// <remarks>
        /// Aluno com suas turmas. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="alunoViewModel">
        /// Aluno com suas turmas
        /// </param>
        /// <returns code="201"></returns>
        public async Task<IHttpActionResult> Post([FromBody] AlunoViewModel alunoViewModel)
        {
            await _alunoService.Adicionar(alunoViewModel.Aluno, alunoViewModel.Turmas);
            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Delte o Aluno
        /// </summary>
        /// <remarks>
        /// Deleta o Aluno sem turmas. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="matricula">
        /// Matricula do aluno
        /// </param>
        /// <returns code="204"></returns>
        [Route("api/aluno/{matricula}")]
        public async Task<IHttpActionResult> Delete(string matricula)
        {
            await _alunoService.Remover(matricula);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Atualiza o Aluno adicionando ou removendo as turmas
        /// </summary>
        /// <remarks>
        /// Atualiza o Aluno. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="alunoViewModel">
        /// Aluno com turmas
        /// </param>
        /// <returns code="201"></returns>
        public async Task<IHttpActionResult> PUT([FromBody] AlunoViewModel alunoViewModel)
        {
            if(alunoViewModel.Turmas == null)
            {
                alunoViewModel.Turmas = new List<Turma>();
            }

            await _alunoService.Atualizar(alunoViewModel.Aluno, alunoViewModel.Turmas);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
