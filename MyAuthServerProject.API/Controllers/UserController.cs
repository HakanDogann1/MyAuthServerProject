using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Services;

namespace MyAuthServerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto)
        {
            var value = await _userService.CreateUserAsync(createUserDto);
            return ActionResultInstance(value);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var value = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return ActionResultInstance(value);
        }
        [HttpPost("{CreateUserRoleAsync}")]
        public async Task<IActionResult> CreateUserRoleAsync(string userName)
        {
            var value = await _userService.CreateUserRoleAsync(userName);
            return ActionResultInstance(value);
        }
    }
}
