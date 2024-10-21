using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Produtos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoProduto(IRepositorioProduto repositorio, IImageService imageService) : IServicoProduto
    {
        private IRepositorioProduto _repositorio = repositorio;
        private IImageService _imageService = imageService;

        public async Task<Response<Produto?>> AdicionarProduto(CriarProdutoRequest request)
        {

            var entity = request.ToEntity();
            var result = await _repositorio.CriarProduto(entity);

            if (!result)
            {
                return new Response<Produto?>(null, 500, "Não foi possivel criar o produto");
            }

            if (request.Imagem is not null)
            {
                if (await TentarAtualizarImage(request.Imagem, entity))
                {
                    result = await _repositorio.Atualizar(entity);
                }
            }


            return result ? new Response<Produto?>(entity, 201, "Novo Produto Criado Com Sucesso") : new Response<Produto?>(entity, 201, "O produto foi criado mas não foi possível adicionar sua foto no momento");
        }

        public async Task<Response<Produto?>> AtualizarProduto(AtualizarProdutoRequest request)
        {
            var entity = await _repositorio.BuscarProduto(request.ProdutoId) ?? throw new NotFoundException("Produto não encontrado");


            string message = "Produto Atualizado Com sucesso";

            entity.Nome = request.Nome;
            entity.Preco = request.Preco;
            entity.Descricao = request.Descricao;
            entity.CategoriaId = request.CategoriaId;

            if (request.Imagem is not null)
            {
                var imageUpdate = await TentarAtualizarImage(request.Imagem, entity);
                if (!imageUpdate)
                {
                    message = "Produto Atualizado, mas houve um problema ao tentar atualizar imagem"; 
                }

            }

            var result = await _repositorio.Atualizar(entity);

            return result ? new Response<Produto?>(entity, 201, message) : new Response<Produto?>(null, 500, "Nao foi possivel atualizar o produto");

        }



        public async Task<Response<Produto?>> BuscarProduto(BuscarProdutoRequest request)
        {
            var entity = await _repositorio.BuscarProduto(request.ProdutoId) ?? throw new NotFoundException("Produto nao encontrado");
            return new Response<Produto?>(entity);

        }

        public async Task<Response<Produto?>> DeletarProduto(RemoverProdutoRequest request)
        {
            var entity = await _repositorio.BuscarProduto(request.ProdutoId) ?? throw new NotFoundException("Produto não encontrado");
            var imagemUrl = entity.Imagem;

            var result = await _repositorio.RemoverProduto(entity);
            if (!result)
            {
                return new Response<Produto?>(null, 500, "Não foi possivel remover o produto no momento");
            }

            await _imageService.RemoverImagem(imagemUrl);

            return new Response<Produto?>(null, 201, "Produto Removido Com Sucesso");
        }

        public async Task<Response<List<Produto>>> ListarProdutos()
        {
            var listaCategorias = await _repositorio.ListarProdutos();

            return new Response<List<Produto>>(listaCategorias);

        }

        private async Task<bool> TentarAtualizarImage(IFormFile? foto, Produto entity)
        {
            var imageUrl = await _imageService.UploadImagem(foto!);

            if (imageUrl is not null)
            {
                entity.Imagem = imageUrl;
                return true;
            }

            return false;
        }
    }
}

