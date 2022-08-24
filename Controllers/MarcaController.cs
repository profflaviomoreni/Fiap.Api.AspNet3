﻿using Fiap.Api.AspNet3.Data;
using Fiap.Api.AspNet3.Models;
using Fiap.Api.AspNet3.Repository;
using Fiap.Api.AspNet3.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.AspNet3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        
        private readonly DataContext dataContext;
        private readonly IMarcaRepository marcaRepository;

        public MarcaController([FromServices] DataContext ctx, [FromServices] IMarcaRepository _marcaRepository)
        {
            dataContext = ctx;
            marcaRepository = _marcaRepository;
        }
        

        /*
        [HttpGet]
        public async Task<ActionResult<IList<MarcaModel>>> Get() // Find All
        {
            var listaMarcas = await dataContext.Marcas.AsNoTracking().ToListAsync<MarcaModel>();

            if ( listaMarcas.Count == 0 )
            {
                return NoContent();
            }


            return Ok(listaMarcas);
        }
        */


        [HttpGet]
        public async Task<ActionResult<IList<dynamic>>> Get(
            [FromQuery] int pagina = 0,
            [FromQuery] int tamanho = 3
        ) // Find All
        {

            var totalGeral = marcaRepository.Count();
            var totalPagina = (int) Math.Ceiling( (double) totalGeral / tamanho );
            var anterior = pagina > 0 ?  $"marca?pagina={pagina-1}&tamanho={tamanho}" : "";
            var proxima = pagina < totalPagina -1 ? $"marca?pagina={pagina + 1}&tamanho={tamanho}" : "";

            if ( pagina > totalPagina)
            {
                return NotFound();
            }
           
            var marcas = marcaRepository.FindAll(pagina, tamanho);

            return Ok(
                new
                {
                    total = totalGeral,
                    totalPaginas = totalPagina,
                    anterior = anterior,
                    proxima = proxima,
                    marcas = marcas
                }    
            );


        }




        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarcaModel>> Get(int id) //Find By Id 
        {

            if ( id == 0 )
            {
                return BadRequest();
            }

            var marca = await dataContext.Marcas.AsNoTracking().FirstOrDefaultAsync( m=> m.MarcaId == id);

            if ( marca == null )
            {
                return NotFound();
            }


            return Ok(marca);
        }

        
        [HttpPost]
        public async Task<ActionResult<MarcaModel>> Post([FromBody] MarcaModel marcaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                dataContext.Marcas.Add(marcaModel);
                await dataContext.SaveChangesAsync();

                var location = new Uri(Request.GetEncodedUrl() + marcaModel.MarcaId);
                return Created(location,marcaModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<MarcaModel>> Put([FromRoute] int id,  [FromBody] MarcaModel marcaModel)
        {
            if ( (id != marcaModel.MarcaId)  || ( id == 0 ) )
            {
                return BadRequest( new { messagem = $"Não foi possível alterar a marca de id = {id}" } );
            }

            if ( ! ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            try
            {
                dataContext.Marcas.Update(marcaModel);
                await dataContext.SaveChangesAsync();

                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<MarcaModel>> Delete([FromRoute] int id)
        {
            var marca = await dataContext.Marcas.FirstOrDefaultAsync(m => m.MarcaId == id);
            
            if( marca == null )
            {
                return NotFound();
            } 
            else
            {
                dataContext.Marcas.Remove(marca);
                await dataContext.SaveChangesAsync();
                return NoContent();
            }

        }


    }
}
