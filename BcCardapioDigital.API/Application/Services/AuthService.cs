using BcCardapioDigital.API.Application.Requests.Auth;
using BcCardapioDigital.API.Application.Responses;
using BcCardapioDigital.API.Domain.Repositories;
using CloudinaryDotNet.Actions;

namespace BcCardapioDigital.API.Application.Services
{
    public class AuthService(IAuthRepositorio repositorio, ITokenService tokenService) : IAuthService
    {
        private readonly IAuthRepositorio _userRepository = repositorio;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Response<string?>> Login(LoginRequest request)
        {
            var usuarioValido = await _userRepository.Login(request.Email, request.Password);
            if (!usuarioValido)
            {
                throw new Exception("Usuario nao encontrado");
            }

            var token = _tokenService.GenerateToken(request.Email);

            return new Response<string?>(token);
        }

        

    }
}
