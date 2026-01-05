using OpenQA.Selenium;
using TestSwagLabs.Exceptions;

namespace TestSwagLabs.Pages;

public class CartPage
{
    private readonly IWebDriver _driver;

    public CartPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public int GetNumberOfItemsInCart()
    {
        IWebElement? element = null;
        try
        {
            element = _driver.FindElement(By.ClassName("shopping_cart_badge"));
        }
        catch (NoSuchElementException)
        {
            return 0;
        }

        return int.Parse(element.Text);
    }

    public int GetNumberOfDisplayedItems()
    {
        var items = _driver.FindElements(By.ClassName("cart_item"));
        return items.Count;
    }

    public void NavigateToItemPageBasedOnItemId(string itemId)
    {
        try
        {
            IWebElement itemLink = _driver.FindElement(By.Id("item_" + itemId + "_title_link"));
            itemLink.Click();
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotFoundException(itemId);
        }
    }

    public List<string> GetAllItemsIdsInCart()
    {
        var itemElements = _driver.FindElements(By.ClassName("cart_item"));
        var itemsIds = new List<string>();

        foreach (var itemElement in itemElements)
        {
            var itemId = itemElement.FindElement(By.ClassName("item_pricebar"))
            .FindElement(By.ClassName("btn_secondary"))
            .GetAttribute("id").Replace("remove-", "");
            itemsIds.Add(itemId);
        }

        return itemsIds;
    }

    public void RemoveItemFromCartByItemId(string itemId)
    {
        IWebElement? removeFromCartButton = null;
        try
        {
            removeFromCartButton = _driver.FindElement(By.Id("remove-" + itemId));
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotAddedBeforeToCartException();
        }

        removeFromCartButton.Click();
    }

    public void NavigateBackToInventoryPage()
    {
        IWebElement? continueShoppingButton = null;
        try
        {
            continueShoppingButton = _driver.FindElement(By.Id("continue-shopping"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Continue Shopping button not found on the Cart Page.");
        }

        continueShoppingButton.Click();
    }

    public void NavigateToCheckOutPage()
    {
        IWebElement? checkoutButton = null;
        try
        {
            checkoutButton = _driver.FindElement(By.Id("checkout"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Checkout button not found on the Cart Page.");
        }

        checkoutButton.Click();
    }

    public void OpenDrawer()
    {
        IWebElement? drawerButton = null;

        try
        {
            drawerButton = _driver.FindElement(By.Id("react-burger-menu-btn"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Drawer button not found.");
        }

        drawerButton.Click();
    }
}
