using Business.Intefaces;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [Route("api/usuario/")]
    public class AuthController : ApiController
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/usuario/create")]
        public async Task<IHttpActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            await _usuarioRepository.Adicionar(usuario);
            return Ok(JwtManager.GenerateToken(usuario.Email));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/usuario/login")]
        public async Task<IHttpActionResult> LoginUsuario([FromBody] Usuario usuario)
        {
            var user = await _usuarioRepository.Buscar(u => u.Email.Equals(usuario.Email) && u.Password.Equals(usuario.Password));
            if(user.Count() == 0) return BadRequest("E-mail ou senha errados");

            return Ok(JwtManager.GenerateToken(usuario.Email));
        }

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _usuarioRepository.Remover(id);
            return Ok("Ok");
        }

        public async Task<IHttpActionResult> Put([FromBody] Usuario usuario)
        {
            await _usuarioRepository.Atualizar(usuario);
            return Ok("Ok");
        }
    }
}
