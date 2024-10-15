using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IServicoRestaurante
    {
        Task<Response<Restaurante>> BuscarRestaurante(); 
        Task<Response<Restaurante?>> AtualizarRestaurante(AtualizarRestauranteRequest request);
        Task<Response<DashBoardResponse>> BuscarDadosDashBoard(DateTime? startDate, DateTime? endDate, int? year);
    }
}
