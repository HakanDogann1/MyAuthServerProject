namespace MyAuthServerProject.Core.DTOs
{
    public class TokenDto
    {
        //Üyelik gereken ksım için kullanılır.
        public string AccessToken { get; set; } //Json Web Tokenin encode edilmiş hali
        public DateTime AccessTokenExpiration { get; set; } //Access tokenın süresi tutulur.

        public string RefreshToken { get; set; } //Refresh Tokenı tutar.
        public DateTime RefreshTokenExpiration { get; set; } // Refresh Tokenın ömrünü tutar.
    }
}
