using OpenQA.Selenium;
using TestSwagLabs.Exceptions;

namespace TestSwagLabs.Pages;

public class CartPage
{
    private readonly IWebDriver _driver;

    public CartPage(IWebDriver driver)
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

    public int GetNumberOfDisplayedItems()
    {
        var items = _driver.FindElements(By.ClassName("cart_item"));
        return items.Count;
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
