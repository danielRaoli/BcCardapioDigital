using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
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
