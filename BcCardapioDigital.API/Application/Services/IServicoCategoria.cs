using BcCardapioDigital.API.Application.Requests.Categorias;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IServicoCategoria
    {
        Task<Response<Categoria?>> AdicionarCategoria(AddCategoriaRequest request);
        Task<Response<Categoria?>> BuscarCategoria(BuscarCategoriaRequest request);
        Task<Response<List<Categoria>>> ListarCategorias();
        Task<Response<Categoria?>> DeletarCategoria(RemoverCategoriaRequest request);
        Task<Response<Categoria?>> AtualizarCategoria(AtualizarCategoriaRequest request);
    }
}
