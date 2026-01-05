using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TestSwagLabs.Pages;

namespace TestSwagLabs.Tests;

public class ItemDetailsTests
{
    OpenQA.Selenium.IWebDriver driver;

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
    public void AddItemToCart_ShouldIncreaseCartItemCount(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        string itemId = inventoryPage.GetRandomItemIdFromInventroyItems();
        inventoryPage.NavigateToItemPageBasedOnItemId(itemId);

        ItemDetailsPage itemDetailsPage = new ItemDetailsPage(driver);
        itemDetailsPage.AddItemToCart();
        int itemCount = itemDetailsPage.GetNumberOfItemsInCart();

        Assert.That(itemCount, Is.EqualTo(1));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void RemoveItemFromCart_ShouldDecreaseCartItemCount(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        string itemId = inventoryPage.GetRandomItemIdFromInventroyItems();
        inventoryPage.NavigateToItemPageBasedOnItemId(itemId);

        ItemDetailsPage itemDetailsPage = new ItemDetailsPage(driver);
        itemDetailsPage.AddItemToCart();
        itemDetailsPage.RemoveItemFromCart();
        int itemCount = itemDetailsPage.GetNumberOfItemsInCart();

        Assert.That(itemCount, Is.EqualTo(0));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void NavigateBackToInventoryPage_ShouldNavigateToInventoryPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        string itemId = inventoryPage.GetRandomItemIdFromInventroyItems();
        inventoryPage.NavigateToItemPageBasedOnItemId(itemId);

        ItemDetailsPage itemDetailsPage = new ItemDetailsPage(driver);
        itemDetailsPage.NavigateBackToInventoryPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.InventoryUrl));
    }

}