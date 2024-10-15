namespace BcCardapioDigital.API.Application.Exceptions
{
    public class NotFoundException(string message) : CardapioDigitalException(message)
    {
        public override List<string> GetMessage()
        {
            return [Message];
        }

        public override int GetStatusCode()
        {
            return StatusCodes.Status404NotFound;
        }
    }
}
