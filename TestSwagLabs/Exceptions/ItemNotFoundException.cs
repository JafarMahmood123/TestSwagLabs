namespace TestSwagLabs.Exceptions;

public class ItemNotFoundException : CustomException
{
    public ItemNotFoundException(string itemId) : base("Item with id:" + itemId + " not found.")
    {
    }
}
