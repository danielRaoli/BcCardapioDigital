namespace BcCardapioDigital.API.Domain.Entities
{
    public class Restaurante
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<HorarioFuncionamento> Horarios { get; set; } = [];
    }
}
