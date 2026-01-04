using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TestSwagLabs.Pages;

namespace TestSwagLabs.Tests;

public class OverViewTests
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
    public void NavigateToCompletePage_ShouldNavigateToOrderCompletePage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.EnterCheckoutInformation("Jafar", "Mahmood", "12345");
        chekOutPage.NavigateToOverViewPage();

        OverViewPage overViewPage = new OverViewPage(driver);
        overViewPage.NavigateToCompletePage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.OrderCompleteUrl));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void NavigateBackToInventoryPage_ShouldNavigateToInventoryPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.EnterCheckoutInformation("Jafar", "Mahmood", "12345");
        chekOutPage.NavigateToOverViewPage();

        OverViewPage overViewPage = new OverViewPage(driver);
        overViewPage.NavigateBackToInventoryPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.InventoryUrl));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", 2)]
    public void CalculateTotalPrice_ShouldReturnTotalPriceIncludingTax(string userName, string password, int numberOfItemsToBeAdded)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.AddNItemsToCart(numberOfItemsToBeAdded);
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.EnterCheckoutInformation("Jafar", "Mahmood", "12345");
        chekOutPage.NavigateToOverViewPage();

        OverViewPage overViewPage = new OverViewPage(driver);
        double totalPrice = overViewPage.CalculateTotalPrice();

        double expectedTotalPrice = overViewPage.GetDisplayedTotalPrice();

        Assert.That(totalPrice, Is.EqualTo(expectedTotalPrice));
    }
}