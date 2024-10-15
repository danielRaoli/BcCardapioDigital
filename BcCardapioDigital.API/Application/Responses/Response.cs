namespace BcCardapioDigital.API.Application.Responses
{
    public class Response<T>(T? data, int code = 200, string message = null)
    {
        private readonly int _code = code;
        public  string? Message { get; private set; } = message;
        public T? Data { get; set; } = data;
        public bool IsSucces => _code > 199 && _code < 300;
    }
}
