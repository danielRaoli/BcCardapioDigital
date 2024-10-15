using Microsoft.IdentityModel.Tokens;

namespace BcCardapioDigital.API.Application.Exceptions
{
    public abstract class CardapioDigitalException(string message) : Exception(message)
    {
        private int _code;
        private List<string> _errors;

        public abstract int GetStatusCode();

        public abstract List<string> GetMessage();
    }
}
