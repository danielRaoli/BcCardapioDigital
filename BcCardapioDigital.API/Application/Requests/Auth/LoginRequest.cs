using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Application.Requests.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Preencha todos os campos obrigatórios")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preencha todos os campos obrigatórios")]
        public string Password { get; set; } = string.Empty;
    }
}
