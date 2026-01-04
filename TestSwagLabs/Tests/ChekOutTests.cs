using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestSwagLabs.Exceptions;
using TestSwagLabs.Pages;

namespace TestSwagLabs.Tests;

public class ChekOutTests
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
    [TestCase("standard_user", "secret_sauce", "Jafar", "Mahmood", "12345")]
    public void NavigateToOverViewPage_ShouldTakeValidCheckoutInformationAndNavigateToOverviewPage(string userName, string password, string firstName, string lastName, string postalCode)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.AddToCartByItemId("sauce-labs-backpack");
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.EnterCheckoutInformation(firstName, lastName, postalCode);
        chekOutPage.NavigateToOverViewPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.OverViewUrl));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce", "Jafar", "Mahmood", "")]
    [TestCase("standard_user", "secret_sauce", "", "Mahmood", "12345")]
    [TestCase("standard_user", "secret_sauce", "Jafar", "", "12345")]
    public void NavigateToOverViewPage_ShouldThrowInvalidCheckOutInformationException(string userName, string password, string firstName, string lastName, string postalCode)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.EnterCheckoutInformation(firstName, lastName, postalCode);

        Assert.Throws<InvalidCheckOutInformationException>(() => chekOutPage.NavigateToOverViewPage());
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void NavigateBackToCartPage_ShouldNavigateBackToCartPageFromCheckoutPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        InventoryPage inventoryPage = new InventoryPage(driver);
        inventoryPage.NavigateToCartItemsPage();

        CartPage cartPage = new CartPage(driver);
        cartPage.NavigateToCheckOutPage();

        ChekOutPage chekOutPage = new ChekOutPage(driver);
        chekOutPage.NavigateBackToCartPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.CartUrl));
    }
}
