using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.ValueObjects;

namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IRepositorioRestaurante
    {
        Task<Restaurante?> Buscar();
        Task<bool> Atualizar(Restaurante entity);

        Task<List<ProdutoMaisVendido>> ProdutosMaisVendido(DateTime? startDate, DateTime? EndDate);
        Task<int> TotalPedidos(DateTime? startDate, DateTime? EndDate);
        Task<decimal> DinheiroRecebido(DateTime? startDate, DateTime? EndDate); 
        Task<Dictionary<int, decimal>> VendasPorMes(int? year);
    }
    
}
