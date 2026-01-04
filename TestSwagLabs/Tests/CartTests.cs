using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestSwagLabs.Pages;

namespace TestSwagLabs.Tests;

public class CartTests
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
    // [TestCase("standard_user", "secret_sauce", 0)]
    [TestCase("standard_user", "secret_sauce", 4)]
    public void GetNumberOfItemsInCart_ShouldMatchTheNumberOnTheCartWithTheNumberOfDisplayedItems(string username, string password, int numberOfItemsToBeAdded)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        var loginPage = new LoginPage(driver);
        loginPage.Login(username, password);

        var inventoryPage = new InventoryPage(driver);
        
        inventoryPage.AddNItemsToCart(numberOfItemsToBeAdded);

        inventoryPage.NavigateToCartItemsPage();

        var cartPage = new CartPage(driver);
        int numberOfItemsInCart = cartPage.GetNumberOfItemsInCart();

        int numberOfDisplayedItems = cartPage.GetNumberOfDisplayedItems();

        Assert.That(numberOfItemsInCart, Is.EqualTo(numberOfDisplayedItems));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void GoToItemPage_ShouldNavigateToItemPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        var itemId = inventoryPage.AddRandomItemToCart();
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToItemPageBasedOnItemId(itemId);

        Assert.IsTrue(driver.Url.Contains(SiteUrls.ItemUrl + itemId));
    }
}