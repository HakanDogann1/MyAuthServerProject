namespace MyAuthServerProject.Core.DTOs
{
    public class ClientLoginDto
    {
        //UserName gibi görev yapacak.
        public string ClientId { get; set; }
        //Şifre gibi görev yapacak.
        public string ClientSecret { get; set; }
    }
}
