using Fiap.Api.AspNet3.Models;
using Fiap.Api.AspNet3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel.Senha.Equals("123456") )
            {
                usuarioModel.NomeUsuario = "Flávio";
                usuarioModel.Regra = "Pleno";

                var tokenUsuario = AuthenticationService.GetToken(usuarioModel);

                return new { 
                    usuario = usuarioModel,
                    token = tokenUsuario
                };

            } else
            {
                return NotFound();
            }

        }


    }
}
