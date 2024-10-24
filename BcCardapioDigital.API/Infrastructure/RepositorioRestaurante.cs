using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using BcCardapioDigital.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class RepositorioRestaurante(AppDbContext context) : IRepositorioRestaurante
    {
        private readonly AppDbContext _context = context;
        public async Task<bool> Atualizar(Restaurante entity)
        {
            _context.Restaurantes.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public Task<Restaurante?> Buscar()
        {
            var restaurante = _context.Restaurantes.FirstOrDefaultAsync();

            return restaurante;
        }

        public async Task<decimal> DinheiroRecebido(DateTime startDate, DateTime endDate)
        {
            var dinheiroRecebido = await _context.Pedidos
                .Where(p => p.Data >= startDate.ToUniversalTime() && p.Data <= endDate.ToUniversalTime() && p.Status != Domain.Enums.Status.Cancelado)
                .SumAsync(p => p.TotalPrice);

            return dinheiroRecebido;
        }

        public async Task<List<ProdutoMaisVendido>> ProdutosMaisVendido(DateTime startDate, DateTime EndDate)
        {
            var produtosMaisVendidos = await _context.ItemPedidos.GroupBy(p => p.ProdutoId)
                .Select(g => new ProdutoMaisVendido
                {
                    ProdutoId = g.Key,
                    Name = _context.Produtos.FirstOrDefault(p => p.Id == g.Key)!.Nome,
                    QuantidadeVendida = g.Sum(p => p.Quantidade)
                }).OrderByDescending(g => g.QuantidadeVendida).Take(10).ToListAsync();

            return produtosMaisVendidos;
        }

        public async Task<int> TotalPedidos(DateTime startDate, DateTime EndDate)
        {
            var totalPedidos = await _context.Pedidos.Where(p => p.Data >= startDate.ToUniversalTime() && p.Data <= EndDate.ToUniversalTime() && p.Status != Domain.Enums.Status.Cancelado).CountAsync();

            return totalPedidos;
        }


        public async Task<Dictionary<int, decimal>> VendasPorMes(int year)
        {
            var vendasPorMes = await _context.Pedidos.Where(p => p.Data.Year == year)
                .GroupBy(p => p.Data.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Vendas = g.Sum(p => p.TotalPrice)
                }).ToDictionaryAsync(d => d.Mes, d => d.Vendas);
            return vendasPorMes;
        }
    }
}
