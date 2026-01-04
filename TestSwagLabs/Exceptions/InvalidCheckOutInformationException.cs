namespace TestSwagLabs.Exceptions;

public class InvalidCheckOutInformationException : CustomException
{
    public InvalidCheckOutInformationException() : base("Invalid checkout information provided.")
    {
    }
}
