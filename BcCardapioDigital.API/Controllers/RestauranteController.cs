using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BcCardapioDigital.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestauranteController(IServicoRestaurante servico) : ControllerBase
    {
        private readonly IServicoRestaurante _servico = servico;

        [HttpGet]
        public async Task<IActionResult> BuscarRestaurante()
        {
            try
            {
                var result = await _servico.BuscarRestaurante();
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AtualizarRestaurante(AtualizarRestauranteRequest request)
        {
            try
            {


                var result = await _servico.AtualizarRestaurante(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        [HttpGet("dashboard")]
        [Authorize]
        public async Task<IActionResult> DadosDashboard([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] int? year )
        {
            var result = await _servico.BuscarDadosDashBoard(startDate, endDate, year); 
            return Ok(result);
        }

    }
}
