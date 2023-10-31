using MyAuthServerProject.Core.Configuration;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Entities;

namespace MyAuthServerProject.Core.Services
{
    public interface ITokenService
    {
        //Kendi iç yapımızda döneceğimiz için response dönmedik
        TokenDto CreateTokenDto(UserApp userApp);

        ClientTokenDto CreateClientTokenDto(Client client);
    }
}
