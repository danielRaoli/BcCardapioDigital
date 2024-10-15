using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class RepositorioCategoria(AppDbContext context) : IRepositorioCategoria
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> Atualizar(Categoria entity)
        {
            _context.Categorias.Update(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Categoria?> BuscarCategoria(long id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CriarCategoria(Categoria entity)
        {
            _context.Categorias.Add(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Categoria>> ListarCategorias()
        {
            return await _context.Categorias.AsNoTracking().Include(c => c.Produtos).ToListAsync();
        }

        public async Task<bool> RemoverCategoria(Categoria entity)
        {
            _context.Categorias.Remove(entity);
            var result = await _context.SaveChangesAsync();

            return result > 0;

        }
    }
}
