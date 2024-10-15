using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BcCardapioDigital.API.Application.Requests.Restaurante
{
    public record AtualizarHorarioRequest
    {
        public DayOfWeek IdDiaSemana { get; set; }

        [Required(ErrorMessage = "Preencha os campos obrigatórios")]

        public string HoraAbertura { get; set; } = string.Empty;

        [Required(ErrorMessage = "Preencha os campos obrigatórios")]

        public string HoraFechamento { get; set; } = string.Empty;
        [Required(ErrorMessage = "Preencha os campos obrigatórios")]
        public bool Funcionando { get; set; }

    }
}
