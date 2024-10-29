using BcCardapioDigital.API.Application.Requests.Produtos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IServicoProduto
    {
        Task<Response<Produto?>> AdicionarProduto(CriarProdutoRequest request);
        Task<Response<Produto?>> BuscarProduto(BuscarProdutoRequest request);
        Task<Response<List<ProdutoResponse>>> ListarProdutos();
        Task<Response<List<Produto>>> ProdutosPopulares();
        Task<Response<Produto?>> DeletarProduto(RemoverProdutoRequest request);
        Task<Response<Produto?>> AtualizarProduto(AtualizarProdutoRequest request);
    }
}
