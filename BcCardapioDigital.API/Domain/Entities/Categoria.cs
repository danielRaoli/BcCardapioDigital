using System.ComponentModel.DataAnnotations;


namespace BcCardapioDigital.API.Domain.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public List<Produto> Produtos { get; set; } = [];
    }
}
