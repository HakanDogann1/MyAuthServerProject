namespace MyAuthServerProject.Core.DTOs
{
    public class ClientTokenDto
    {
        //Üyelik gerekmeyen kısım için kullanılacak
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiretion { get; set; }
    }
}
