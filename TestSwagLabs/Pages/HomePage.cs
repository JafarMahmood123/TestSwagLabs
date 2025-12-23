using OpenQA.Selenium;

namespace TestSwagLabs.Pages;

public class HomePage
{
    private readonly IWebDriver _driver;

    public HomePage(IWebDriver driver)
    {
        _driver = driver;
    }
    public void AddToCart(string itemId)
    {
        IWebElement addToCartButton = _driver.FindElement(By.Id(itemId));
        addToCartButton.Click();
    }
}
