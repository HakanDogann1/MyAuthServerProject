using Microsoft.AspNetCore.Identity;

namespace MyAuthServerProject.Core.Entities
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; } //AppUser tablosuna ek olarak City bilgisi de eklenecek.
    }
}
