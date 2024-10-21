using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoRestaurante(IRepositorioRestaurante repositorio) : IServicoRestaurante
    {
        private readonly IRepositorioRestaurante _repositorio = repositorio;
        private static DateTime _firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private static DateTime _lastDay = _firstDay.AddMonths(1).AddDays(-1);
        private static int _year = DateTime.Now.Year;
        public async Task<Response<Restaurante?>> AtualizarRestaurante(AtualizarRestauranteRequest request)
        {
            var restaurante = await _repositorio.Buscar() ?? throw new NotFoundException("Restaurante nao encontrado");

            restaurante.Nome = request.Nome;    
            restaurante.Endereco = request.Endereco;
            restaurante.Telefone = request.Telefone;
            restaurante.Email = request.Email;

            var result = await _repositorio.Atualizar(restaurante);

            return result ?
           new Response<Restaurante?>(null, 200, "Restaurante Atualizado Com Sucesso") : new Response<Restaurante?>(null, 500, "Nao foi possivel atualizar os dados do restaurante no momento");
        }

        public async Task<Response<DashBoardResponse>> BuscarDadosDashBoard(DateTime? startDate = null, DateTime? endDate = null, int? year = null)
        {
            startDate ??= _firstDay;
            endDate ??= _lastDay;
            year ??= _year;

            var totalPedidos = await _repositorio.TotalPedidos(startDate, endDate);
            var dinheiroRecebido = await _repositorio.DinheiroRecebido(startDate, endDate);
            var produtosMaisVendidos = await _repositorio.ProdutosMaisVendido(startDate, endDate);
            var dinheiroAcumuladoPorMes = await _repositorio.VendasPorMes(year); 

            var dashBoard = new DashBoardResponse { TotalPedidos = totalPedidos, TotalVendas = dinheiroRecebido, ProdutosMaisVendidos =  produtosMaisVendidos, ValoresArrecadadosMes = dinheiroAcumuladoPorMes };
            return new Response<DashBoardResponse>(dashBoard);
        }

        public async Task<Response<Restaurante>> BuscarRestaurante()
        {
            var restaurante = await _repositorio.Buscar() ?? throw new NotFoundException("Restaurante nao encontrado");

            return new Response<Restaurante>(restaurante);
        }
    }
}
