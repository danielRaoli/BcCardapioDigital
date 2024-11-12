using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Produtos;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoProduto(IRepositorioProduto repositorio, IImageService imageService, IMemoryCache memoryCache) : IServicoProduto
    {
        private IRepositorioProduto _repositorio = repositorio;
        private IImageService _imageService = imageService;
        private IMemoryCache _memoryCache = memoryCache;

        public async Task<Response<Produto?>> AdicionarProduto(CriarProdutoRequest request)
        {

            var entity = request.ToEntity();
            var result = await _repositorio.CriarProduto(entity);

            if (!result)
            {
                return new Response<Produto?>(null, 500, "Não foi possivel criar o produto");
            }

            _memoryCache.Remove("cacheprodutos");

            if (request.Imagem is not null)
            {
                if (await TentarAtualizarImage(request.Imagem, entity))
                {
                    result = await _repositorio.Atualizar(entity);
                }
            }

            if (!result)
            {

                return new Response<Produto?>(entity, 201, "O produto foi criado mas não foi possível adicionar sua foto no momento");

            }
           

            return new Response<Produto?>(entity, 201, "Novo Produto Criado Com Sucesso");
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

            if (!result)
            {
                return new Response<Produto?>(null, 500, "Nao foi possivel atualizar o produto");
            }

            _memoryCache.Remove("cacheprodutos");


            return new Response<Produto?>(entity, 201, message);

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

            _memoryCache.Remove("cacheprodutos");

            if (!string.IsNullOrEmpty(imagemUrl) && !imagemUrl.Equals("imagempadrao")) await _imageService.RemoverImagem(imagemUrl);

            

            return new Response<Produto?>(null, 200, "Produto Removido Com Sucesso");
        }

        public async Task<Response<List<ProdutoResponse>>> ListarProdutos()
        {
            var cacheProdutoos = await _memoryCache.GetOrCreateAsync("cacheprodutos", async entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(240);
                var listaProduto = await _repositorio.ListarProdutos();

                var listaProdutoResponse = listaProduto.Select(p => ProdutoResponse.FromEntity(p)).ToList();

                return listaProdutoResponse;

            });
            

            return new Response<List<ProdutoResponse>>(cacheProdutoos);

        }

        public async Task<Response<List<Produto>>> ProdutosPopulares()
        {
            var produtoCache = await _memoryCache.GetOrCreateAsync("cacheprodutospopular", async entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(240);

                var response = await _repositorio.ProdutosPopulares();

                return response;
            });

            return new Response<List<Produto>>(produtoCache);
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

