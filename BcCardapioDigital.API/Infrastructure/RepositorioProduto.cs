using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using BcCardapioDigital.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class RepositorioProduto(AppDbContext context) : IRepositorioProduto
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> Atualizar(Produto entity)
        {
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == entity.CategoriaId);

            if (!categoriaExiste)
            {
                return false;
            }
            _context.Produtos.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Produto?> BuscarProduto(long id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CriarProduto(Produto entity)
        {
            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == entity.CategoriaId);

            if (!categoriaExiste)
            {
                return false;
            }

            await _context.Produtos.AddAsync(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<List<Produto>> ProdutosPopulares()
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToUniversalTime();

            var query = _context.ItemPedidos
                    .Join(
                        _context.Pedidos,
                        itemPedido => itemPedido.PedidoId,
                        pedido => pedido.Id,
                        (itemPedido, pedido) => new { itemPedido, pedido }
                    )
                    .AsQueryable();


            query = query.Where(p => p.pedido.Data >= firstDay  && p.pedido.Data <= lastDay);


            // Agrupa os produtos pelos mais vendidos e obtém a contagem de vendas
            var produtosMaisVendidos = await query
                .GroupBy(p => p.itemPedido.ProdutoId)
                .Select(g => new
                {
                    ProdutoId = g.Key,
                    TotalVendas = g.Count() // Contagem de vendas por produto
                })
                .OrderByDescending(g => g.TotalVendas) // Ordena pelos mais vendidos
                .Take(8) // Pega os 8 mais vendidos
                .ToListAsync();

            // Recupera os detalhes dos produtos mais vendidos
            var produtos = new List<Produto>();
            foreach (var item in produtosMaisVendidos)
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == item.ProdutoId);
                if (produto != null)
                {
                    produtos.Add(produto);
                }
            }

            return produtos;
        }

        public async Task<List<Produto>> ListarProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<bool> RemoverProduto(Produto entity)
        {
            _context.Produtos.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;

        }
    }
}
