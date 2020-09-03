using Api.Filters;
using Api.ViewModels;
using Business.Intefaces;
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

        public async Task<IHttpActionResult> Get()
        {
            var alunos = await _alunoService.ObterAlunosComTurmas();
            return Ok(alunos);
        }

        [Route("api/aluno/{matricula}")]
        public async Task<IHttpActionResult> Get(string matricula)
        {
            var aluno = await _alunoService.ObterAlunoComTurmas(matricula);
            return Ok(aluno);
        }

        public async Task<IHttpActionResult> Post([FromBody] AlunoViewModel alunoViewModel)
        {
            await _alunoService.Adicionar(alunoViewModel.Aluno, alunoViewModel.Turmas);
            return StatusCode(HttpStatusCode.Created);
        }

        [Route("api/aluno/{matricula}")]
        public async Task<IHttpActionResult> Delete(string matricula)
        {
            await _alunoService.Remover(matricula);
            return StatusCode(HttpStatusCode.Created);
        }

        public async Task<IHttpActionResult> PUT([FromBody] AlunoViewModel alunoViewModel)
        {
            await _alunoService.Atualizar(alunoViewModel.Aluno, alunoViewModel.Turmas);
            return Ok("PUT");
        }
    }
}
