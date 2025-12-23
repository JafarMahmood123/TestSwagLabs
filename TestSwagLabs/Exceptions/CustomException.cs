namespace TestSwagLabs.Exceptions;

public class CustomException : Exception
{
    private string _message;
    public CustomException(string message) : base(message)
    {
        _message = message;
    }
}
