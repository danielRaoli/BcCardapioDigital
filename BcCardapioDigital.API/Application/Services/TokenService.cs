using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BcCardapioDigital.API.Application.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        public string GenerateToken(string email)
        {

            var key = _configuration["TokenConfiguration:SecurityKey"];

            var encodedKey = Encoding.ASCII.GetBytes(key);

            var tokenHandle = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Email, email),
                ]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256)

            };

            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
