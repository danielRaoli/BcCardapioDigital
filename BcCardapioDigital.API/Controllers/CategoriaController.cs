using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Categorias;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BcCardapioDigital.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController(IServicoCategoria servicoCategoria) : ControllerBase
    {
        private readonly IServicoCategoria _servicoCategoria = servicoCategoria;

        [HttpPost]
        public async Task<IActionResult> Post(AddCategoriaRequest request)
        {

            var result = await _servicoCategoria.AdicionarCategoria(request);
            return Created("", result);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var request = new BuscarCategoriaRequest { CategoriaId = id };
                var result = await _servicoCategoria.BuscarCategoria(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _servicoCategoria.ListarCategorias();
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, AtualizarCategoriaRequest request)
        {
            try
            {

                request.CategoriaId = id;
                var result = await _servicoCategoria.AtualizarCategoria(request);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var request = new RemoverCategoriaRequest { CategoriaId = id };
                var result = await _servicoCategoria.DeletarCategoria(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }
    }
}
