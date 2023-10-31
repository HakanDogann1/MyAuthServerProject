using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Services;

namespace MyAuthServerProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result =await _authenticationService.CreateTokenAsync(loginDto);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto loginDto)
        {
            var result = _authenticationService.CreateTokenByClient(loginDto);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshToken)
        {
            var value = await _authenticationService.RevokeRefreshToken(refreshToken.Token);
            return ActionResultInstance(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRefreshToken(RefreshTokenDto refreshToken)
        {
            var value = await _authenticationService.CreateRefreshTokenAsync(refreshToken.Token);
            return ActionResultInstance(value);
        }
    }
}
