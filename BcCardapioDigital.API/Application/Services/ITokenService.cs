namespace BcCardapioDigital.API.Application.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string email);
    }
}
