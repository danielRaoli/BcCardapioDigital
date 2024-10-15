using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Produtos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BcCardapioDigital.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController(IServicoProduto servico) : ControllerBase
    {
        private readonly IServicoProduto _servico = servico;

        [HttpPost]
        public async Task<IActionResult> Post(CriarProdutoRequest request)
        {
            try
            {


                var result = await _servico.AdicionarProduto(request);
                return Created("", result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var request = new BuscarProdutoRequest { ProdutoId = id };
                var result = await _servico.BuscarProduto(request);
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
            var result = await _servico.ListarProdutos();
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, AtualizarProdutoRequest request)
        {
            try
            {

                request.ProdutoId = id;
                var result = await _servico.AtualizarProduto(request);

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
                var request = new RemoverProdutoRequest { ProdutoId = id };
                var result = await _servico.DeletarProduto(request);
                return Ok(result);

            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response<string>(null, ex.GetStatusCode(), ex.Message));
            }

        }
    }
}
