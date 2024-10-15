
using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IServicoHorario
    {
        Task<Response<HorarioFuncionamento?>> Atualizar(AtualizarHorarioRequest request);
        Task<Response<HorarioFuncionamento?>> Buscar(BuscarHorarioRequest request);
        Task<Response<List<HorarioFuncionamento>>> BuscarTodos();
    }
}
