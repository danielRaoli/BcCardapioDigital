using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Pedidos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Application.Services;
using BcCardapioDigital.API.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BcCardapioDigital.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PedidoController(IServicoPedido servico) : ControllerBase
    {
        private readonly IServicoPedido _servico = servico;

        [HttpPost]
        public async Task<IActionResult> EfetuarPedido([FromBody] FazerPedidoRequest request)
        {
            try
            {

                var result = await _servico.FazerPedido(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        
        [HttpPost("buscarpedido")]
        public async Task<IActionResult> BuscarPedido([FromBody] BuscarPedidoRequest request)
        {
            try
            {


                var result = await _servico.BuscarPedido(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));

            }

        }

        [Authorize]
        [HttpGet("buscarpedidosdia")]
        public async Task<IActionResult> BuscarPedidosDoDia([FromQuery] Status status)
        {
            var request = new BuscarPedidosDoDiaRequest { Status = status };
            var result = await _servico.BuscarPedidos(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("buscartodospedidos")]
        public async Task<IActionResult> BuscarTodosOsPedidos()
        {
            var result = await _servico.BuscarPedidos();
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AtualizarStatusDoPedido([FromBody] AtualizarStatusPedidoRequest request)
        {
            try
            {

                var result = await _servico.AtualizarStatus(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }
    }
}
