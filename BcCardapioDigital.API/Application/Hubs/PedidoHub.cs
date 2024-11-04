using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace BcCardapioDigital.API.Application.Hubs
{
    public class PedidoHub : Hub
    {
        public async Task EnviarAtualizacaoPedido(string pedidoId, Status status)
        {
            await Clients.All.SendAsync("ReceberAtualizacaoPedido", pedidoId, status);
        }

        public async Task ReceberNovoPedido(Pedido pedido)
        {
            await Clients.All.SendAsync("ReceberNovoPedido", pedido);
        }
    }
}
