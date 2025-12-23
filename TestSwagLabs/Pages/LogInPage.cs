using OpenQA.Selenium;

namespace TestSwagLabs;

internal class LoginPage
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Login(string userName, string password)
    {
        IWebElement userNameText = _driver.FindElement(By.Name("user-name"));
        userNameText.SendKeys(userName);

        IWebElement passwordText = _driver.FindElement(By.Name("password"));
        passwordText.SendKeys(password);

        IWebElement loginButton = _driver.FindElement(By.Id("login-button"));
        loginButton.Click();
    }
}
