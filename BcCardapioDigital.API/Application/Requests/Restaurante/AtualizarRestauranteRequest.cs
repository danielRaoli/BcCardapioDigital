using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Restaurante
{
    public record AtualizarRestauranteRequest
    {
        public int RestauranteId { get; set; }
        [Required(ErrorMessage = "Preencha os campos obrigatório")]
        [MaxLength(150, ErrorMessage = "O nome do restaurante não pode ter mais que 150 caracteres")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Preencha os campos obrigatório")]
        public string Endereco { get; set; } = string.Empty;
        [Required(ErrorMessage = "Preencha os campos obrigatório")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preencha os campos obrigatório")]
        [EmailAddress(ErrorMessage ="O Email precisa ser válido")]
        public string Email { get; set; } = string.Empty;
    }
}
