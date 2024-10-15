using BcCardapioDigital.API.Domain.Enums;

namespace BcCardapioDigital.API.Application.Requests.Restaurante
{
    public record BuscarHorarioRequest
    {
        public DayOfWeek Dia { get; set; }
    }
}
