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

        public async Task<IHttpActionResult> Get()
        {
            var turmas = await _turmaService.ObterTurmasComAlunos();
            return Ok(turmas);
        }

        [Route("api/turma/{codigo}")]
        public async Task<IHttpActionResult> Get(string codigo)
        {
            var turma = await _turmaService.ObterTurmaComAlunos(codigo);
            return Ok(turma);
        }
     
        public async Task<IHttpActionResult> Post([FromBody] Turma turma)
        {
            await _turmaService.Adicionar(turma);

            return StatusCode(HttpStatusCode.Created);
        }

        [Route("api/turma/{codigo}")]
        public async Task<IHttpActionResult> Delete(string codigo)
        {
            await _turmaService.Remover(codigo);
            return Ok("Delete");
        }

        public async Task<IHttpActionResult> PUT([FromBody] Turma turma)
        {
            await _turmaService.Atualizar(turma);
            return Ok("PUT");
        }
    }
}
