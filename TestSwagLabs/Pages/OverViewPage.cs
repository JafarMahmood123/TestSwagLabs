using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V141.Debugger;

namespace TestSwagLabs.Pages;

public class OverViewPage
{
    private readonly IWebDriver _driver;

    public OverViewPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void NavigateBackToInventoryPage()
    {
        var finishButton = _driver.FindElement(By.Id("cancel"));
        finishButton.Click();
    }

    public void NavigateToCompletePage()
    {
        var finishButton = _driver.FindElement(By.Id("finish"));
        finishButton.Click();
    }

    public void NavigateToItemPageBasedOnItemId(string itemId)
    {
        var itemLink = _driver.FindElement(By.Id(itemId));
        itemLink.Click();
    }

    public double CalculateTotalPrice()
    {
        double totalPrice = 0.0;
        var cartItems = _driver.FindElements(By.ClassName("cart_item"));
        foreach (var cartItem in cartItems)
        {
            var item = cartItem.FindElement(By.ClassName("inventory_item_price"));
            var itemsQuantity = cartItem.FindElement(By.ClassName("cart_quantity"));

            var quantity = int.Parse(itemsQuantity.Text);
            var priceText = double.Parse(item.Text.Replace("$", ""));
            totalPrice += priceText * quantity;
        }

        var tax = _driver.FindElement(By.ClassName("summary_tax_label"));
        var taxValueText = tax.Text.Replace("Tax: $", "");
        totalPrice += double.Parse(taxValueText);

        return totalPrice;
    }

    public double GetDisplayedTotalPrice()
    {
        var totalElement = _driver.FindElement(By.ClassName("summary_total_label"));
        var totalText = totalElement.Text.Replace("Total: $", "");
        return double.Parse(totalText);
    }
}
