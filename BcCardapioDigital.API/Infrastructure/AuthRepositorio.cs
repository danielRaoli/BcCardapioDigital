using BcCardapioDigital.API.Domain.Entities;
using BcCardapioDigital.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BcCardapioDigital.API.Infrastructure
{
    public class AuthRepositorio(AppDbContext context) : IAuthRepositorio
    {
        private readonly AppDbContext _context = context;
        public async Task<bool> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return  user != null;

        }
    }
}
