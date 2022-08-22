using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
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
