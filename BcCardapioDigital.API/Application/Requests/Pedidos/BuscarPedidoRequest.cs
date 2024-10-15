namespace BcCardapioDigital.API.Application.Requests.Pedidos
{
    public record BuscarPedidoRequest
    {
        public string CodigoProduto { get; set; } = string.Empty;
    }
}
