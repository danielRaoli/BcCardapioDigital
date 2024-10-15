using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IRepositorioCategoria
    {
        Task<bool> CriarCategoria(Categoria entity);
        Task<Categoria?> BuscarCategoria(long id);
        Task<List<Categoria>> ListarCategorias();
        Task<bool> RemoverCategoria(Categoria entity);
        Task<bool> Atualizar(Categoria entity);
    }
}
