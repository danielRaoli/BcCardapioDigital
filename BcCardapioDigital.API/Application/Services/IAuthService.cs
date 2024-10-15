using BcCardapioDigital.API.Application.Requests.Auth;
using BcCardapioDigital.API.Application.Responses;

namespace BcCardapioDigital.API.Application.Services
{
    public interface IAuthService
    {
        Task<Response<string?>> Login(LoginRequest loginRequest);
    }
}
