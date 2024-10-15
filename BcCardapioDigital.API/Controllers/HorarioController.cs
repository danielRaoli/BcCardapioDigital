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
    [Authorize]
    public class HorarioController(IServicoHorario servico) : ControllerBase
    {

        private readonly IServicoHorario _servico = servico;
        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarHorario([FromRoute] int id,AtualizarHorarioRequest request)
        {
            try
            {
                request.IdDiaSemana = (DayOfWeek)id;
                var result = await _servico.Atualizar(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }
            catch (Exception) 
            {
                return StatusCode(500,new Response<string>(null, 500, "Internal server Error"));
            }

        }

        [HttpGet("horario")]
        public async Task<IActionResult> BuscarHorario([FromQuery] DayOfWeek DiaSemana)
        {
            try
            {
                var request = new BuscarHorarioRequest { Dia = DiaSemana };
                var result = await _servico.Buscar(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarTodos()
        {

            var result = await _servico.BuscarTodos();
            return Ok(result);
        }
    }
}
