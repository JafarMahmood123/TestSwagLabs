using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestSwagLabs.Pages;

public class DrawerPage
{
    private readonly IWebDriver _driver;

    public DrawerPage(IWebDriver driver)
    {
        _driver = driver;
    }
    public void NavigateToInventoryPage()
    {
        var inventoryLinkLocator = By.Id("inventory_sidebar_link");

        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(inventoryLinkLocator));
            element.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new WebDriverTimeoutException("Inventory link in drawer was not clickable within 5 seconds.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to navigate to Inventory Page.", ex);
        }
    }

    public void NavigateToAboutPage()
    {

        var aboutLinkLocator = By.Id("about_sidebar_link");

        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(aboutLinkLocator));
            element.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new WebDriverTimeoutException("About link in drawer was not clickable within 5 seconds.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to navigate to About Page.", ex);
        }
    }

    public void LogOut()
    {
        var logOutLinkLocator = By.Id("logout_sidebar_link");

        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(logOutLinkLocator));
            element.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new WebDriverTimeoutException("Log out link in drawer was not clickable within 5 seconds.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to navigate to log in Page.", ex);
        }
    }

    public void ResetAppState()
    {
        var resetLinkLocator = By.Id("reset_sidebar_link");

        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(resetLinkLocator));
            element.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new WebDriverTimeoutException("Reset app state in drawer was not clickable within 5 seconds.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to reset app state.", ex);
        }
    }

    public void CloseDrawer()
    {
        var closeButtonLocator = By.Id("react-burger-cross-btn");

        try
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(closeButtonLocator));
            element.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new WebDriverTimeoutException("Close drawer link in drawer was not clickable within 5 seconds.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to close drawer.", ex);
        }
    }
}
