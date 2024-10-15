using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Infrastructure
{
    public class DbSeed(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task SeedAsync()
        {
            // Verifica se já existe um restaurante no banco de dados
            if (!_context.Restaurantes.Any())
            {
                var restaurante = new Restaurante
                {
                    Nome = "Restaurante Exemplo",
                    Endereco = "Rua Exemplo, 123",
                    Telefone = "(11) 98765-4321",
                    Email = "contato@restauranteexemplo.com"
                };

                _context.Restaurantes.Add(restaurante);
                await _context.SaveChangesAsync();

                var horariosFuncionamento = new List<HorarioFuncionamento>
            {
                    new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Sunday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Monday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Tuesday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Wednesday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Thursday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Friday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                },
                new HorarioFuncionamento
                {
                    DiaSemana = DayOfWeek.Saturday,
                    HoraAbertura = new TimeSpan(9, 0, 0),
                    HoraFechamento = new TimeSpan(18, 0, 0),
                    Funcionando = true,
                    RestauranteId = restaurante.Id
                }

            };

                _context.Horarios.AddRange(horariosFuncionamento);

                var user = new User { Email = "bcrestaurante@email.com", Password = "senha123" };
                _context.Users.Add(user);

                await _context.SaveChangesAsync();





            }
        }
    }
}
