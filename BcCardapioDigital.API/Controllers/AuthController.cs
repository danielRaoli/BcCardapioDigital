using BcCardapioDigital.API.Application.Exceptions;
using BcCardapioDigital.API.Application.Requests.Auth;
using BcCardapioDigital.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcCardapioDigital.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService  service) : ControllerBase
    {
        private readonly IAuthService _userService = service;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _userService.Login(loginRequest);

                return Ok(response);
            }
            catch (NotFoundException) 
            {
                return Unauthorized();  
            }

        }
    }
}
