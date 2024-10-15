using BcCardapioDigital.API.Domain.ValueObjects;

namespace BcCardapioDigital.API.Application.Responses
{
    public record DashBoardResponse
    {
        public decimal TotalVendas { get; set; }
        public int TotalPedidos { get; set; }
        public List<ProdutoMaisVendido> ProdutosMaisVendidos { get; set; } = [];
        public Dictionary<int,decimal> ValoresArrecadadosMes { get; set; } = [];
    }

}
