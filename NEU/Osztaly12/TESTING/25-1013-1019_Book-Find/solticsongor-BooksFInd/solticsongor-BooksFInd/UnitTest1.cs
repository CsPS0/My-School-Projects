using OpenQA.Selenium;

namespace solticsongor_BooksFInd;

public class Tests
{
    private SeleniumHandler seleniumHandler = new SeleniumHandler();

    [SetUp]
    public void Setup()
    {
       seleniumHandler.Setup();
    }

    [TearDown]
    public void TearDown()
    {
        seleniumHandler.Close();
    }

    [Test]
    public void CheckTitle()
    {
        seleniumHandler.driver?.Navigate().GoToUrl("https://books.toscrape.com/");
        Assert.That(seleniumHandler.driver?.Title, Is.EqualTo("All products | Books to Scrape - Sandbox"));
    }

    [Test]
    public void CheckHumorCategory()
    {
        seleniumHandler.driver?.Navigate().GoToUrl("https://books.toscrape.com/");
        var humorCategory = seleniumHandler.driver?.FindElement(By.LinkText("Humor"));
        Assert.That(humorCategory?.Text, Is.EqualTo("Humor"));
    }

    [Test]
    public void CheckBookPrice()
    {
        seleniumHandler.driver?.Navigate().GoToUrl("https://books.toscrape.com/");
        var book = seleniumHandler.driver?.FindElement(By.CssSelector("a[title='A Light in the Attic']"));
        book?.Click();
        var price = seleniumHandler.driver?.FindElement(By.ClassName("price_color"));
        Assert.That(price?.Text, Is.EqualTo("£51.77"));
    }
}