using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BcCardapioDigital.API.Application.Requests.Categorias
{
    public record AtualizarCategoriaRequest
    {
        [JsonIgnore]
        public int CategoriaId { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "A categoria deve entre 3 e 150 caracteres")]
        public string Nome { get; set; } = string.Empty;
        public IFormFile? Imagem { get; set; }
    }
}
