using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Restaurante;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoRestaurante(IRepositorioRestaurante repositorio, IMemoryCache memoryCache) : IServicoRestaurante
    {
        private readonly IRepositorioRestaurante _repositorio = repositorio;
        private IMemoryCache _memoryCache = memoryCache;

        private static DateTime _firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        private static DateTime _lastDay = _firstDay.AddMonths(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToUniversalTime();
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

        public async Task<Response<DashBoardResponse>> BuscarDadosDashBoard(DateTime? startDate , DateTime? endDate, int? year)
        {
            startDate ??= _firstDay;
            endDate ??= _lastDay;
            year ??= _year;

            var dashBoardCache = await _memoryCache.GetOrCreateAsync("cachedashboard", async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddHours(10);
                var totalPedidos = await _repositorio.TotalPedidos(startDate.Value, endDate.Value);
                var dinheiroRecebido = await _repositorio.DinheiroRecebido(startDate.Value, endDate.Value);
                var produtosMaisVendidos = await _repositorio.ProdutosMaisVendido(startDate.Value, endDate.Value);
                var dinheiroAcumuladoPorMes = await _repositorio.VendasPorMes(year.Value);

                var dashBoard = new DashBoardResponse { TotalPedidos = totalPedidos, TotalVendas = dinheiroRecebido, ProdutosMaisVendidos = produtosMaisVendidos, ValoresArrecadadosMes = dinheiroAcumuladoPorMes };

                return dashBoard;
            });

            
            return new Response<DashBoardResponse>(dashBoardCache);
        }

        public async Task<Response<Restaurante>> BuscarRestaurante()
        {
            var restauranteCache = await _memoryCache.GetOrCreateAsync("cacherestaurante", async entry =>
            {
                var restaurante = await _repositorio.Buscar() ?? throw new NotFoundException("Restaurante nao encontrado");

                return restaurante;
            });
            

            return new Response<Restaurante>(restauranteCache);
        }
    }
}
