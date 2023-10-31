using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAuthServerProject.Core.Configuration;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Entities;
using MyAuthServerProject.Core.Repositories;
using MyAuthServerProject.Core.Services;
using SharedLibrary.Configuration;
using SharedLibrary.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _customTokenOptions;
        public TokenService(UserManager<UserApp> userManager , IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            _customTokenOptions = options.Value;
        }

        public string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        public async Task<IEnumerable<Claim>> GetClaims(UserApp userApp , List<string> audiences)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                  new Claim("city",userApp.City),
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }
        private IEnumerable<Claim> GetClaimsByClient(Client client) {
            var claims = new List<Claim>();
            claims.AddRange(client.Audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub,client.UserId.ToString());
            return claims;
        }
        public ClientTokenDto CreateClientTokenDto(Client client)
        {
            var accessTokenExpriration = DateTime.Now.AddDays(_customTokenOptions.AccessTokenExpiration);
            var securityKey = SignInService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _customTokenOptions.Issuer,
                expires: accessTokenExpriration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiretion = accessTokenExpriration,
            };
            return tokenDto;
        }

        public TokenDto CreateTokenDto(UserApp userApp)
        {
            var accessTokenExpriration = DateTime.Now.AddDays(_customTokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddDays(_customTokenOptions.RefreshTokenExpiration);
            var securityKey = SignInService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _customTokenOptions.Issuer,
                expires: accessTokenExpriration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _customTokenOptions.Audience).Result,
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpriration,
                RefreshTokenExpiration = refreshTokenExpiration,
                RefreshToken = CreateRefreshToken()

            };
            return tokenDto;
        }
    }
}
