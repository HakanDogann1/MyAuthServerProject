namespace MyAuthServerProject.Core.Entities
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } //Bu refresh token kime ait onu tutacak.
        public string Code { get; set; } //Refresh Token alacak.

        public DateTime Expiretion { get; set; }//Refresh Tokenın ömrünü alacak
    }
}
