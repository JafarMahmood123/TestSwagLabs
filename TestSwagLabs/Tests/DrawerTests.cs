using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V143.Runtime;
using TestSwagLabs.Pages;

namespace TestSwagLabs.Tests;

public class DrawerTests
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
    public void NavigateToInventoryPage_ShouldNavigateToInventoryPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var inventoryPage = new InventoryPage(driver);
        inventoryPage.NavigateToCartItemsPage();

        var cartPage = new CartPage(driver);
        cartPage.OpenDrawer();

        DrawerPage drawerPage = new DrawerPage(driver);
        drawerPage.NavigateToInventoryPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.InventoryUrl));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void LogOut_ShouldNavigateToLoginPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var inventoryPage = new InventoryPage(driver);
        inventoryPage.OpenDrawer();

        DrawerPage drawerPage = new DrawerPage(driver);
        drawerPage.LogOut();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.SiteUrl));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void ResetAppState_ShouldClearCartItems(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var inventoryPage = new InventoryPage(driver);
        inventoryPage.AddRandomItemToCart();
        inventoryPage.OpenDrawer();

        DrawerPage drawerPage = new DrawerPage(driver);
        drawerPage.ResetAppState();
        drawerPage.CloseDrawer();

        var items = driver.FindElements(By.ClassName("inventory_item"));

        bool isReseted = true;

        foreach (var item in items)
        {
            try
            {
                var removeButton = item.FindElement(By.ClassName("btn_inventory"));
                if (removeButton.Text.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    isReseted = false;
                    break;
                }
            }
            catch (NoSuchElementException)
            {
                continue;
            }

        }

        Assert.That(isReseted, Is.EqualTo(true));
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void CloseDrawer_ShouldCloseTheDrawer(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var inventoryPage = new InventoryPage(driver);
        inventoryPage.OpenDrawer();

        DrawerPage drawerPage = new DrawerPage(driver);
        drawerPage.CloseDrawer();

        Assert.That(inventoryPage.IsDrawerClosed(), Is.True);
    }

    [Test]
    [TestCase("standard_user", "secret_sauce")]
    public void NavigateToAboutPage_ShouldNavigateToAboutPage(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);
        LoginPage loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        var inventoryPage = new InventoryPage(driver);
        inventoryPage.OpenDrawer();

        DrawerPage drawerPage = new DrawerPage(driver);
        drawerPage.NavigateToAboutPage();

        Assert.That(driver.Url, Is.EqualTo(SiteUrls.AboutUrl));
    }
}
