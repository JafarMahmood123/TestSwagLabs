using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestSwagLabs.Pages;


namespace TestSwagLabs;

internal class LogInTest
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");

        // These are useful for stability on Linux/Containers
        options.AddArgument("--width=1920");
        options.AddArgument("--height=1080");

        // Initialize the Chrome Driver
        driver = new ChromeDriver(options);
    }


    [TearDown]
    public void Teardown()
    {
        driver.Dispose();
    }


    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void LogIn_ShouldLogInTheUserWithUserNameAndPassword(string userName, string password)
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        IWebElement userNameText = driver.FindElement(By.Name("user-name"));
        userNameText.SendKeys(userName);

        IWebElement passwordText = driver.FindElement(By.Name("password"));
        passwordText.SendKeys(password);


        IWebElement loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();

        Assert.IsTrue(driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
    }

    [Test]
    [TestCase("locked_out_user", "secret_sauce")]
    public void LogIn_ShouldNotLogInTheUserWithUserNameAndPassword(string userName, string password)
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        IWebElement userNameText = driver.FindElement(By.Name("user-name"));
        userNameText.SendKeys(userName);

        IWebElement passwordText = driver.FindElement(By.Name("password"));
        passwordText.SendKeys(password);


        IWebElement loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();

        Assert.IsTrue(!driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "sauce-labs-backpack")]
    public void AddToCart_ShouldAddToChartWhenLoggedIn(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var itemsInCartBefore = 0;

        IWebElement? element = null;
        try
        {
            element = driver.FindElement(By.ClassName("shopping_cart_badge"));
        }
        catch (NoSuchElementException)
        {
            TestContext.Progress.WriteLine("No items in the cart before adding.");
        }

        if (element == null)
            itemsInCartBefore = 0;
        else
            itemsInCartBefore = int.Parse(element.Text);

        IWebElement addToCartButton = driver.FindElement(By.Id("add-to-cart-" + itemId));
        addToCartButton.Click();

        IWebElement cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
        int itemsInCartAfter = int.Parse(cartBadge.Text);

        Assert.That(itemsInCartAfter, Is.EqualTo(itemsInCartBefore + 1));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "sauce-labs-backpack")]
    public void RemoveFromCart_ShouldRemoveFromCartWhenLoggedIn(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        HomePage homePage = new HomePage(driver);
        homePage.AddToCart("add-to-cart-" + itemId);

        IWebElement element = driver.FindElement(By.ClassName("shopping_cart_badge"));

        var itemsInCartBefore = int.Parse(element.Text);

        IWebElement removeFromCartButton = driver.FindElement(By.Id("remove-" + itemId));
        removeFromCartButton.Click();

        IWebElement? cartBadge = null;
        int itemsInCartAfter = 0;
        try
        {
            cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
        }
        catch (NoSuchElementException)
        {
            TestContext.Progress.WriteLine("No items in the cart before adding.");
        }

        if (cartBadge == null)
            itemsInCartAfter = 0;
        else
            itemsInCartAfter = int.Parse(cartBadge.Text);
        

        Assert.That(itemsInCartAfter, Is.EqualTo(itemsInCartBefore - 1));
    }
}
