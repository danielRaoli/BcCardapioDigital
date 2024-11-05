namespace BcCardapioDigital.API.Application.Requests.Pedidos
{
    public record BuscarPedidoRequest
    {
        public string CodigoPedido { get; set; } = string.Empty;
    }
}
