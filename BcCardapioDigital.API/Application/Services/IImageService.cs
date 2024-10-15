    namespace BcCardapioDigital.API.Application.Services
{
    public interface IImageService
    {
        Task<string?> UploadImagem(IFormFile imagem);
        Task<bool> RemoverImagem(string publicId);
    }
}
