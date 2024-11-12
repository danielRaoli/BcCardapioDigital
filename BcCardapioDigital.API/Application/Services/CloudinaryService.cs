using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace BcCardapioDigital.API.Application.Services
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudinaryConfig = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            _cloudinary = new Cloudinary(cloudinaryConfig);
        }

        public async Task<string?> UploadImagem(IFormFile imagem)
        {
            using var stream = imagem.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imagem.FileName, stream),
                Folder = "produtos",
                PublicId = Guid.NewGuid().ToString()
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.StatusCode == System.Net.HttpStatusCode.OK ? uploadResult.SecureUrl?.AbsoluteUri : null;

        }

        public async Task<bool> RemoverImagem(string imageUrl)
        {

            var startIndex = imageUrl.IndexOf("produtos/") + "produtos/".Length;
            var endIndex = imageUrl.IndexOf('.', startIndex);
            var imageId = imageUrl.Substring(startIndex, endIndex - startIndex);

            var deletionParams = new DeletionParams (imageId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }
    }
}
