namespace MyAuthServerProject.Core.Configuration
{
    public class Client
    {
        //Dto kullanmadık çünkü UI tarafında değil kod tarafında kullanılacak.
        //Server a istek yapacak alan. (SPA,Web ve Mobil ... olabilir)
        public string UserId { get; set; }
        public string Secret { get; set; }

        //SPA apiden hangilerine erişeceği bilgisini tutarız.(www.myapi1 www.myapi2 gibi)
        public List<String> Audience { get; set; }
    }
}
