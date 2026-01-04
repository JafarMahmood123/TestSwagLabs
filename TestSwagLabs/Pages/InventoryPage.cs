using OpenQA.Selenium;
using TestSwagLabs.Exceptions;

namespace TestSwagLabs.Pages;

public class InventoryPage
{
    private readonly IWebDriver _driver;

    public InventoryPage(IWebDriver driver)
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

    public void AddToCartByItemId(string itemId)
    {
        IWebElement? addToCartButton = null;
        try
        {
            addToCartButton = _driver.FindElement(By.Id("add-to-cart-" + itemId));
        }
        catch (NoSuchElementException)
        {
            throw new ItemNotFoundException(itemId);
        }

        addToCartButton.Click();
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

    public List<string> GetAllItemNamesInInventory()
    {
        List<IWebElement>? inventoryContainer = null;

        try
        {
            inventoryContainer = _driver.FindElements(By.ClassName("inventory_item_name")).ToList();
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Inventory container not found.");
        }

        return inventoryContainer.Select(item => item.Text).ToList();
    }

    public string? GetFilteringType()
    {
        IWebElement? filterDropdown = null;

        try
        {
            filterDropdown = _driver.FindElement(By.ClassName("product_sort_container"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Filter dropdown not found.");
        }

        return filterDropdown.GetAttribute("value");
    }

    public void SetFilteringType(string filterType)
    {
        IWebElement? filterDropdown = null;

        try
        {
            filterDropdown = _driver.FindElement(By.ClassName("product_sort_container"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Filter dropdown not found.");
        }

        try
        {
            IWebElement option = filterDropdown.FindElement(By.CssSelector($"option[value='{filterType}']"));
            option.Click();
        }
        catch (NoSuchElementException)
        {
            throw new Exception($"Filter type '{filterType}' not found.");
        }
    }

    public List<double> GetAllItemPricesInInventory()
    {
        List<IWebElement>? inventoryContainer = null;

        try
        {
            inventoryContainer = _driver.FindElements(By.ClassName("inventory_item_price")).ToList();
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Inventory container not found.");
        }

        return inventoryContainer.Select(item => double.Parse(item.Text.Substring(1))).ToList();
    }

    public void ViewCartItems()
    {
        IWebElement? cartIcon = null;

        try
        {
            cartIcon = _driver.FindElement(By.ClassName("shopping_cart_link"));
        }
        catch (NoSuchElementException)
        {
            throw new Exception("Cart icon not found.");
        }

        cartIcon.Click();
    }
}
