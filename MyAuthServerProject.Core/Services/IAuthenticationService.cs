using MyAuthServerProject.Core.DTOs;
using SharedLibrary.Dtos;

namespace MyAuthServerProject.Core.Services
{
    public interface IAuthenticationService
    {
        //Direk api ile haberleşecek servisdir.
        //Yeni Token oluşturulacak.
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        //Yeni refresh token oluşturulacak.
        Task<Response<TokenDto>> CreateRefreshTokenAsync(string refreshToken);
        //Kullanıcı logout olduğunda refresh tokenı sıfırlayabiliriz.
        Task<Response<NoContent>> RevokeRefreshToken(string refreshToken);
        //Client için bir token oluşturmak için

        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto loginDto);
    }
}
