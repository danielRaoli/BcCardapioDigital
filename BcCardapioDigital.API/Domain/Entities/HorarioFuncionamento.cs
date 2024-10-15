using System.ComponentModel.DataAnnotations;

namespace BcCardapioDigital.API.Domain.Entities
{
    public class HorarioFuncionamento
    {
        [Key]
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraFechamento { get; set; }
        public bool Funcionando { get; set; }
        public int RestauranteId { get; set; }
    }
}
