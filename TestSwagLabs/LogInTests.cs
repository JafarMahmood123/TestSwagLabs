using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace TestSwagLabs;

internal class LogInTests
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
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);

        var loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        Assert.IsTrue(driver.Url.Equals(SiteUrls.InventoryUrl));
    }

    [Test]
    [TestCase("locked_out_user", "secret_sauce")]
    public void LogIn_ShouldNotLogInTheUserWithUserNameAndPassword(string userName, string password)
    {
        driver.Navigate().GoToUrl(SiteUrls.SiteUrl);

        var loginPage = new LoginPage(driver);
        loginPage.Login(userName, password);

        Assert.IsTrue(!driver.Url.Equals(SiteUrls.InventoryUrl));
    }
}
