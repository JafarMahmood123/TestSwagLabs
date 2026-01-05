using OpenQA.Selenium;

namespace TestSwagLabs.Pages;

public class ItemDetailsPage
{
    private readonly IWebDriver _driver;

    public ItemDetailsPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void NavigateBackToInventoryPage()
    {
        var backButton = _driver.FindElement(By.Id("back-to-products"));
        backButton.Click();
    }

    public void AddItemToCart()
    {
        var addToCartButton = _driver.FindElement(By.Id("add-to-cart"));
        addToCartButton.Click();
    }

    public void RemoveItemFromCart()
    {
        var removeButton = _driver.FindElement(By.Id("remove"));
        removeButton.Click();
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
}
