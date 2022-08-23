using Fiap.Api.AspNet3.Clients;
using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public async Task<string> GetAsync()
        {
            var client = new CursoClient();

            var model = new CursoModel()
            {
                Nome = "T1",
                Concluido = true,
                Nivel = "N1",
                PercentualConclusao = 10,
                Preco = 1.25,
                Conteudo = "C1"
            };

            int retorno = await client.Insert(model);


            var cursos = client.Get().Result;

            return "Olá Fiap - GET";
        }

        [HttpPost]
        public string Post()
        {
            return "Olá Fiap - POST";
        }

        [HttpPut]
        public string Put()
        {
            return "Olá Fiap - PUT";
        }

        [HttpDelete]
        public string Delete()
        {
            return "Olá Fiap - DELETE";
        }


    }
}
