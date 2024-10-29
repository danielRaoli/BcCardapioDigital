using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Responses
{
    public record ProdutoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Imagem { get; set; } = string.Empty;
        public string NomeCategoria { get; set; } = string.Empty;
        public int CategoriaId { get; set; }


        public static ProdutoResponse FromEntity(Produto produto) =>
            new ProdutoResponse
            {
                CategoriaId = produto.CategoriaId,
                Nome = produto.Nome,
                Id = produto.Id,
                NomeCategoria = produto.Categoria.Nome,
                Descricao = produto.Descricao,
                Imagem = produto.Imagem,
                Preco = produto.Preco
            };
    }
}
