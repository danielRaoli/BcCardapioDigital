namespace BcCardapioDigital.API.Domain.ValueObjects
{
    public record ProdutoMaisVendido
    {

        public int ProdutoId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantidadeVendida { get; set; }

    }
}
