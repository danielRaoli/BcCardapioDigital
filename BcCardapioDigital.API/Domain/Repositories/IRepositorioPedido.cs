using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Enums;

namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IRepositorioPedido
    {
        Task<bool> AtualizarPedido(Pedido pedido);
        Task<Pedido?> BuscarPedidoPorId(int pedidoId);
        Task<Pedido?> BuscarPedidoPorCodigo(string codigoPedido);
        Task<List<Pedido>> BuscarPedidos(Status status, DateTime diaAtual);
        Task<List<Pedido>> BuscarPedidos();
        Task<string> CriarPedido(Pedido pedido);
    }
}
