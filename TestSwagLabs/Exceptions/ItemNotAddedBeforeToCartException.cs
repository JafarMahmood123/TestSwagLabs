namespace TestSwagLabs.Exceptions;

public class ItemNotAddedBeforeToCartException : CustomException
{
    public ItemNotAddedBeforeToCartException()
        : base("No items were added to the cart before attempting to remove an item.")
    {
    }
}
