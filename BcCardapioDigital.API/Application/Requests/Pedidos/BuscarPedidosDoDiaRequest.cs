using BcCardapioDigital.API.Domain.Enums;

namespace BcCardapioDigital.API.Application.Requests.Pedidos
{
    public record BuscarPedidosDoDiaRequest
    {
        public Status Status  { get; set; }
        public DateTime DiaAtual { get; set; } = DateTime.UtcNow.AddHours(-3);
    }
}
