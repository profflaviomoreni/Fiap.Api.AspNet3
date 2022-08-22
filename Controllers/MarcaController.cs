using Fiap.Api.AspNet3.Data;
using Fiap.Api.AspNet3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        
        private readonly DataContext dataContext;

        public MarcaController([FromServices] DataContext ctx)
        {
            dataContext = ctx;
        }
        


        [HttpGet]
        public IList<MarcaModel> Get() // Find All
        {
            var listaMarcas = dataContext.Marcas.AsNoTracking().ToList<MarcaModel>();
            return listaMarcas;
        }



        [HttpGet("{id:int}")]
        public MarcaModel Get(int id) //Find By Id 
        {
            var marca = dataContext.Marcas.AsNoTracking().FirstOrDefault( m=> m.MarcaId == id);

            return marca;
        }


    }
}
