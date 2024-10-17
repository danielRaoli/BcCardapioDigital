using BcCardapioDigital.API.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Produtos
{
    public record CriarProdutoRequest
    {

        [Required(ErrorMessage = "Preencha os campos obrigatórios")]
        [MaxLength(150, ErrorMessage = "O nome do Produto deve entre 3 e 150 caracteres")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Preencha os campos obrigatórios")]
        [MaxLength(200, ErrorMessage = "A categoria deve entre 3 e 200 caracteres")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Preencha os campos obrigatórios")]
        public decimal Preco { get; set; }
        public IFormFile? Imagem { get; set; }

        [Required(ErrorMessage = "Preencha os campos obrigatórios")]
        public int CategoriaId { get; set; }

        public Produto ToEntity() => new() { Nome = this.Nome, Descricao = this.Descricao, Preco = this.Preco, CategoriaId = this.CategoriaId, Imagem = "imagempadrao" };
    }
}
