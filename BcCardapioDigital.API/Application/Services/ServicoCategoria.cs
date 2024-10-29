using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Categorias;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace BcCardapioDigital.API.Application.Services
{
    public class ServicoCategoria(IRepositorioCategoria repositorio, IImageService imageService, IMemoryCache memoryCache) : IServicoCategoria
    {
        private IRepositorioCategoria _repositorio = repositorio;
        private IImageService _imageService = imageService;
        private IMemoryCache _memoryCache = memoryCache;

        public async Task<Response<Categoria?>> AdicionarCategoria(AddCategoriaRequest request)
        {

            var entity = request.ToEntity();
            var result = await _repositorio.CriarCategoria(entity);

            if (!result)
            {
                return new Response<Categoria?>(null, 500, "Não foi possivel criar o categoria");
            }

            if (request.Imagem is not null)
            {
                if (await TentarAtualizarImage(request.Imagem, entity))
                {
                    result = await _repositorio.Atualizar(entity);
                }
            }

            if (!result)
            {
                return new Response<Categoria?>(entity, 201, "A Categoria foi criada mas não foi possível adicionar sua foto no momento");
            }

            _memoryCache.Remove("cachecategorias");

            return new Response<Categoria?>(entity, 201, "Nova Categoria Criado Com Sucesso");
        }

        public async Task<Response<Categoria?>> AtualizarCategoria(AtualizarCategoriaRequest request)
        {
            var entity = await _repositorio.BuscarCategoria(request.CategoriaId) ?? throw new NotFoundException("Categoria não encontrado");


            string message = "Categoria Atualizada Com sucesso";

            entity.Nome = request.Nome;

            if (request.Imagem is not null)
            {
                var imageUpdate = await TentarAtualizarImage(request.Imagem, entity);
                if (!imageUpdate)
                {
                    message = "Categoria Atualizada, mas houve um problema ao tentar atualizar imagem";
                }

            }

            var result = await _repositorio.Atualizar(entity);

            if (!result)
            {
                return new Response<Categoria?>(null, 500, "Nao foi possivel atualizar a categoria");
            }

            _memoryCache.Remove("cachecategorias");

            return new Response<Categoria?>(entity, 201, message);
        }



        public async Task<Response<Categoria?>> BuscarCategoria(BuscarCategoriaRequest request)
        {

            var entity = await _repositorio.BuscarCategoria(request.CategoriaId) ?? throw new NotFoundException("Categoria nao encontrada");
            return new Response<Categoria?>(entity, 201, " Categoria Atualizada Com Sucesso");

        }

        public async Task<Response<Categoria?>> DeletarCategoria(RemoverCategoriaRequest request)
        {
            var entity = await _repositorio.BuscarCategoria(request.CategoriaId) ?? throw new NotFoundException("Categoria nao encontrada");
            //se a categoria possuir produtos
            if (entity.Produtos.Count > 0)
            {
                return new Response<Categoria?>(null, 400, "Não é possivel remover uma categoria que tem produtos, mova os produtos para outra categoria ou remova-os");
            }

            var result = await _repositorio.RemoverCategoria(entity);
            if (!result)
            {
                return new Response<Categoria?>(null, 500, "Nao foi possivel atualizar a categoria");
            }
            var imagemUrl = entity.Imagem;
            result = await _imageService.RemoverImagem(imagemUrl);
            if (!result)
            {
                return new Response<Categoria?>(null, 201, " Categoria foi removida, mas nao foi possivel apagar sua imagem");
            }

            return new Response<Categoria?>(null, 201, " Categoria Removida Com Sucesso");
        }

        public async Task<Response<List<Categoria>>> ListarCategorias()
        {
            var categorias = await _memoryCache.GetOrCreateAsync("cachecategorias", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(240);


                var listaCategorias = await _repositorio.ListarCategorias();
                return listaCategorias;
            });

            return new Response<List<Categoria>>(categorias);
        }

        private async Task<bool> TentarAtualizarImage(IFormFile? foto, Categoria entity)
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

