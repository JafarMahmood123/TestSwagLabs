using OpenQA.Selenium;
using TestSwagLabs.Exceptions;

namespace TestSwagLabs.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;

    public InventoryPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public int GetNumberOfItemsInCart()
    {
        IWebElement? element = null;
        try
        {
            element = _driver.FindElement(By.ClassName("shopping_cart_badge"));
        }
        catch (NoSuchElementException)
        {
            return 0;
        }

        return int.Parse(element.Text);
    }

    public void AddToCartByItemId(string itemId)
    {
        IWebElement? addToCartButton = null;
        try
        {
            addToCartButton = _driver.FindElement(By.Id("add-to-cart-" + itemId));
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotFoundException(itemId);
        }

        addToCartButton.Click();
    }

    public void RemoveItemFromCartByItemId(string itemId)
    {
        IWebElement? removeFromCartButton = null;
        try
        {
            removeFromCartButton = _driver.FindElement(By.Id("remove-" + itemId));
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotAddedBeforeToCartException();
        }

        removeFromCartButton.Click();
    }

    public void NavigateToItemPageBasedOnItemId(string itemId)
    {
        try
        {
            IWebElement itemLink = _driver.FindElement(By.Id("item_" + itemId + "_title_link"));
            itemLink.Click();
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotFoundException(itemId);
        }
    }
}
