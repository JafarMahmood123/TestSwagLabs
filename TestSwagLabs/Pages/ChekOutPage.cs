using OpenQA.Selenium;
using TestSwagLabs.Exceptions;

namespace TestSwagLabs.Pages;

public class ChekOutPage
{
    private readonly IWebDriver _driver;

    public ChekOutPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void EnterCheckoutInformation(string firstName, string lastName, string postalCode)
    {
        var firstNameInput = _driver.FindElement(By.Id("first-name"));
        var lastNameInput = _driver.FindElement(By.Id("last-name"));
        var postalCodeInput = _driver.FindElement(By.Id("postal-code"));

        firstNameInput.SendKeys(firstName);
        lastNameInput.SendKeys(lastName);
        postalCodeInput.SendKeys(postalCode);
    }

    public void NavigateToOverViewPage()
    {
        var firstNameInput = _driver.FindElement(By.Id("first-name"));
        var lastNameInput = _driver.FindElement(By.Id("last-name"));
        var postalCodeInput = _driver.FindElement(By.Id("postal-code"));

        if (string.IsNullOrEmpty(firstNameInput.GetAttribute("value")) ||
            string.IsNullOrEmpty(lastNameInput.GetAttribute("value")) ||
            string.IsNullOrEmpty(postalCodeInput.GetAttribute("value")))
        {
            throw new InvalidCheckOutInformationException();
        }

        var continueButton = _driver.FindElement(By.Id("continue"));
        continueButton.Click();
    }

    public void NavigateBackToCartPage()
    {
        var cancelButton = _driver.FindElement(By.Id("cancel"));
        cancelButton.Click();
    }

}
