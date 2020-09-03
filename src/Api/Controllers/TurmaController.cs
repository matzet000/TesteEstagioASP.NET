using Api.Filters;
using Business.Intefaces;
using Business.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [ErrorFilter]
    [JwtAuthentication]
    public class TurmaController : ApiController
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaRepository turmaRepository, IAlunoTurmaRepository alunoTurmaRepository, ITurmaService turmaService)
        {
            _turmaRepository = turmaRepository;
            _alunoTurmaRepository = alunoTurmaRepository;
            _turmaService = turmaService;
        }

        /// <summary>
        /// Obtem todos as turmas com seus alunos
        /// </summary>
        /// <remarks>
        /// As turmas com os alunos. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param>
        /// </param>
        /// <returns code="200"></returns>
        public async Task<IHttpActionResult> Get()
        {
            var turmas = await _turmaService.ObterTurmasComAlunos();
            return Ok(turmas);
        }

        /// <summary>
        /// Obtem a turma com seus alunos
        /// </summary>
        /// <remarks>
        /// Turma com os alunos. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="codigo">
        /// Codigo da turma
        /// </param>
        /// <returns code="200"></returns>
        [Route("api/turma/{codigo}")]
        public async Task<IHttpActionResult> Get(string codigo)
        {
            var turma = await _turmaService.ObterTurmaComAlunos(codigo);
            return Ok(turma);
        }

        /// <summary>
        /// Cadastra uma turma
        /// </summary>
        /// <remarks>
        /// Cadastra uma turma. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="turma">
        /// Codigo da turma e Nome da turma
        /// </param>
        /// <returns code="201"></returns>
        public async Task<IHttpActionResult> Post([FromBody] Turma turma)
        {
            await _turmaService.Adicionar(turma);

            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Deleta uma turma
        /// </summary>
        /// <remarks>
        /// Deleta uma turma. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="codigo">
        /// Codigo da turma
        /// </param>
        /// <returns code="204"></returns>
        [Route("api/turma/{codigo}")]
        public async Task<IHttpActionResult> Delete(string codigo)
        {
            await _turmaService.Remover(codigo);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Atualiza uma turma
        /// </summary>
        /// <remarks>
        /// Atualiza uma turma. Colocar token JWT na HEADER.
        /// </remarks>
        /// <param name="turma">
        /// Codigo da turma e Nome da turma
        /// </param>
        /// <returns code="204"></returns>
        public async Task<IHttpActionResult> PUT([FromBody] Turma turma)
        {
            await _turmaService.Atualizar(turma);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
