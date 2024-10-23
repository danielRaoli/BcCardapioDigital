using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IRepositorioProduto
    {
        Task<bool> CriarProduto(Produto entity);
        Task<Produto?> BuscarProduto(long id);
        Task<List<Produto>> ListarProdutos();
        Task<List<Produto>> ProdutosPopulares();
        Task<bool> RemoverProduto(Produto entity);
        Task<bool> Atualizar(Produto entity);
    }
}
