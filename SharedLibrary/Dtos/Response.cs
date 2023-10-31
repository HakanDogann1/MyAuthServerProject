using System.Text.Json.Serialization;

namespace SharedLibrary.Dtos
{
    public class Response<T> where T : class
    {
        public T Data { get; private set; } //Data dönmek için kullanıldı.
        public int StatusCode { get; private set; } //Statü kodu dönecek(200,400...)
        [JsonIgnore]
        public bool IsSuccessful { get; private set; } //Kullanıcının görmeyeceği kısım , proje içinde kolaylık sağlaması için kullanıldı. İşlem başarılı mı değil mi ?
        public ErrorDto ErrorDto { get; set; }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new Response<T> { ErrorDto = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            ErrorDto errorDto = new ErrorDto(errorMessage, isShow);
            return new Response<T> { ErrorDto = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
