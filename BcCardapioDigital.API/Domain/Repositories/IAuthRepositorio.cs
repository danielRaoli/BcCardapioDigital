namespace BcCardapioDigital.API.Domain.Repositories
{
    public interface IAuthRepositorio
    {
        Task<bool> Login(string username, string password); 
    }
}
