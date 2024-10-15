using BcCardapioDigital.API.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Categorias
{
    public record AddCategoriaRequest
    {
        [Required(ErrorMessage = "Preencha todos os campos obrigatórios")]
        [MaxLength(150, ErrorMessage = "A categoria deve entre 3 e 150 caracteres")]
        public string Nome { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }

        public Categoria ToEntity() => new() { Nome = Nome, ImageUrl = "imagempadrao" };

        
    }
}   
