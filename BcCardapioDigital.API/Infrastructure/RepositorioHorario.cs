using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class RepositorioHorario(AppDbContext context) : IRepositorioHorario
    {
        private readonly AppDbContext _context = context;
        public async Task<bool> Atualizar(HorarioFuncionamento entity)
        {
            _context.Horarios.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<HorarioFuncionamento?> Buscar(DayOfWeek idDiaSemana)
        {
            var diaSemanaFuncionamento = await _context.Horarios.FirstOrDefaultAsync(h => h.DiaSemana == idDiaSemana);

            return diaSemanaFuncionamento;
        }

        public async Task<List<HorarioFuncionamento>> BuscarTodos()
        {
            var todosDias = await _context.Horarios.ToListAsync();
            return todosDias;
        }
    }
}
