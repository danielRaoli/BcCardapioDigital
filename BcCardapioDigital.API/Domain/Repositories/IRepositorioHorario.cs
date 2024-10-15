
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IRepositorioHorario
    {
        Task<HorarioFuncionamento?> Buscar(DayOfWeek idDiaSemana);
        Task<bool> Atualizar(HorarioFuncionamento entity);
        Task<List<HorarioFuncionamento>> BuscarTodos();
    }
}
