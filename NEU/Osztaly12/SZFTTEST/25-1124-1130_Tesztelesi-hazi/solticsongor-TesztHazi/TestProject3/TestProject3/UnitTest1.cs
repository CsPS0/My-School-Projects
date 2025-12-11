using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TestProject3;

[TestClass]
public class UnitTest1
{
    WebDriver _driver;
    [TestInitialize]
    public void Setup()
    {
        FirefoxOptions options = new FirefoxOptions();
        options.AddArgument("--headless");
        FirefoxDriver foxDriver = new FirefoxDriver(options);
        _driver = foxDriver;
        _driver.Navigate().GoToUrl("https://books.toscrape.com/index.html");
    }

    [TestCleanup]
    public void Teardown()
    {
        _driver.Quit();
    }
    
    [TestMethod]
    public void FindFantasy()
    {
        var fantasy = _driver.FindElement(By.XPath("/html/body/div/div/div/aside/div[2]/ul/li/ul/li[18]/a"));
        fantasy.Click();
        var find = _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/h1")).Text;
        Assert.AreEqual("Fantasy",  find);
    }

    [TestMethod]
    public void SumBookPrices()
    {
        _driver.FindElement(By.XPath("/html/body/div/div/div/aside/div[2]/ul/li/ul/li[18]/a")).Click();
        double totalSum = 0;

        while (true)
        {
            var books = _driver.FindElements(By.CssSelector(".product_pod .price_color"));
            foreach (var book in books)
            {
                string bookPriceString = book.Text;
                double bookPrice = Convert.ToDouble(bookPriceString.Replace("£", ""));
                totalSum += bookPrice;
            }

            var nextButton = _driver.FindElements(By.CssSelector(".pager .next a"));
            if (nextButton.Count > 0)
            {
                nextButton[0].Click();
            }
            else
            {
                break;
            }
        }
        Assert.AreEqual(1900.51, totalSum);
    }

    [TestMethod]
    public void SumBookQuantities()
    {
        _driver.FindElement(By.XPath("/html/body/div/div/div/aside/div[2]/ul/li/ul/li[18]/a")).Click();
        int totalQuantity = 0;

        while (true)
        {
            var bookLinks = _driver.FindElements(By.CssSelector(".product_pod h3 a"));

            for (int i = 0; i < bookLinks.Count; i++)
            {
                bookLinks = _driver.FindElements(By.CssSelector(".product_pod h3 a"));
                bookLinks[i].Click();

                var stockElement = _driver.FindElement(By.CssSelector(".product_main .availability"));
                string stockText = stockElement.Text;
                int quantity = ExtractQuantityFromString(stockText);
                totalQuantity += quantity;

                _driver.Navigate().Back();
            }

            var nextButton = _driver.FindElements(By.CssSelector(".pager .next a"));
            if (nextButton.Count > 0)
            {
                nextButton[0].Click();
            }
            else
            {
                break;
            }
        }
        Assert.AreEqual(372, totalQuantity);
    }

    private int ExtractQuantityFromString(string stockText)
    {
        int startIndex = stockText.IndexOf('(') + 1;
        int endIndex = stockText.IndexOf(' ', startIndex);
        string quantityString = stockText.Substring(startIndex, endIndex - startIndex);
        return Convert.ToInt32(quantityString);
    }
}