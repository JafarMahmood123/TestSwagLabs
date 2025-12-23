namespace TestSwagLabs.Exceptions;

public class NoItemsAddedBeforeToCartException : CustomException
{
    public NoItemsAddedBeforeToCartException()
        : base("No items were added to the cart before attempting to remove an item.")
    {
    }
}
