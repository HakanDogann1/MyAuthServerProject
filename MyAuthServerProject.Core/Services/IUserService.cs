using MyAuthServerProject.Core.DTOs;
using SharedLibrary.Dtos;

namespace MyAuthServerProject.Core.Services
{
    public interface IUserService
    {
        //Kullanıcı kayıt işlemi , kullanıcı adına göre kullanıcı sorhulama , role oluşturma
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<NoContent>> CreateUserRoleAsync(string userName);
    }
}
