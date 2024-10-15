using BcCardapioDigital.API.Application.Requests.Pedidos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IServicoPedido
    {
        Task<Response<string?>> FazerPedido(FazerPedidoRequest request);
        Task<Response<Pedido>> BuscarPedido(BuscarPedidoRequest request);
        Task<Response<List<Pedido>>> BuscarPedidos(BuscarPedidosDoDiaRequest request);
        Task<Response<List<Pedido>>> BuscarPedidos();
        Task<Response<Pedido>> AtualizarStatus(AtualizarStatusPedidoRequest request);
    }
}
