using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestSwagLabs.Exceptions;
using TestSwagLabs.Pages;

namespace TestSwagLabs;

internal class InventoryTests
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
    [TestCase("standard_user", "secret_sauce", "sauce-labs-backpack")]
    public void AddToCart_ShouldAddToChartWhenLoggedIn(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);


        InventoryPage inventoryPage = new InventoryPage(driver);
        int itemsInCartBeforeAdding = inventoryPage.GetNumberOfItemsInCart();

        inventoryPage.AddToCartByItemId(itemId);
        int itemsInCartAfterAdding = inventoryPage.GetNumberOfItemsInCart();

        Assert.That(itemsInCartAfterAdding, Is.EqualTo(itemsInCartBeforeAdding + 1));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "sauce-labs-backpack")]
    public void RemoveFromCart_ShouldRemoveFromCartWhenLoggedIn(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.AddToCartByItemId(itemId);

        int itemsInCartBeforeRemoving = inventoryPage.GetNumberOfItemsInCart();

        inventoryPage.RemoveItemFromCartByItemId(itemId);

        int itemsInCartAfterRemoving = inventoryPage.GetNumberOfItemsInCart();
        

        Assert.That(itemsInCartAfterRemoving, Is.EqualTo(itemsInCartBeforeRemoving - 1));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "sauce-labs-backpack")]
    public void RemoveFromCart_ThrowsNoItemsAddedBeforeToCartException(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);

        Assert.Throws<NoItemsAddedBeforeToCartException>(() => inventoryPage.RemoveItemFromCartByItemId(itemId));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "0")]
    public void GoToItemPage_ShouldNavigateToItemPageWhenLoggedIn(string userName, string password, string itemId)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage homePage = new InventoryPage(driver);
        homePage.NavigateToItemPageBasedOnItemId(itemId);

        Assert.IsTrue(driver.Url.Contains(SiteUrls.ItemUrl + itemId));
    }
}
