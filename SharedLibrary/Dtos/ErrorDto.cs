namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; set; } = new List<String>(); //Liste olarak hata alacak.
        public bool IsShow { get; set; } //Kullanıcıya gösterilsin mi ?

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<String> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
