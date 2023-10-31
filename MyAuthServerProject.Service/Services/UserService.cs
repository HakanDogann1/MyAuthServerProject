using Microsoft.AspNetCore.Identity;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Entities;
using MyAuthServerProject.Core.Services;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            if(createUserDto == null)
            {
                return Response<UserAppDto>.Fail("Kullanıcı adı veya şifrenizi giriniz.",400,true);
            }
            var user=new UserApp { Email = createUserDto.Email , UserName=createUserDto.UserName};
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, false),400);
            }
           return Response<UserAppDto>.Success(new UserAppDto { Email=createUserDto.Email,UserName=createUserDto.UserName,Id=user.Id},200);
        }

        public async Task<Response<NoContent>> CreateUserRoleAsync(string userName)
        {
            if(!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
                await _roleManager.CreateAsync(new() { Name = "manager" });
            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "manager");

            return Response<NoContent>.Success(200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Response<UserAppDto>.Fail("Kullanıcı adı bulunamadı", 400, true);
            }
            return Response<UserAppDto>.Success(new UserAppDto { City=user.City,Email=user.Email,UserName=user.UserName,Id=user.Id},200);
        }
    }
}
