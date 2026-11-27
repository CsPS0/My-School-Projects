using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Test;

public class SwaggerUiTests
{
    private IWebDriver Driver = null!;
    private WebDriverWait Wait = null!;

    string baseUrl = "https://fakerestapi.azurewebsites.net";

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1400,1000");
        // no Chrome on this machine: fall back to Brave (Chromium-based, same major version as ChromeDriver)
        const string brave = "/opt/brave.com/brave-origin-nightly/brave-origin";
        if (!File.Exists("/usr/bin/google-chrome") && File.Exists(brave))
            options.BinaryLocation = brave;

        Driver = new ChromeDriver(options);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
        Driver.Navigate().GoToUrl(baseUrl + "/index.html");
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
        Driver.Dispose();
    }

    [Test]
    public void PageTitle()
    {
        Assert.That(Driver.Title, Is.EqualTo("Swagger UI"));
        var heading = Wait.Until(d => d.FindElement(By.CssSelector(".info .title")));
        Assert.That(heading.Text, Does.StartWith("FakeRESTApi.Web V1"));
    }

    [Test]
    public void BooksSectionHasOperations()
    {
        Wait.Until(d => d.FindElement(By.Id("operations-tag-Books")));
        Assert.That(Driver.FindElements(By.Id("operations-Books-get_api_v1_Books")), Has.Count.EqualTo(1));
        Assert.That(Driver.FindElements(By.Id("operations-Books-get_api_v1_Books__id_")), Has.Count.EqualTo(1));
        Assert.That(Driver.FindElements(By.Id("operations-Books-delete_api_v1_Books__id_")), Has.Count.EqualTo(1));
    }

    [Test]
    public void TryItOutGetBook()
    {
        var op = Wait.Until(d => d.FindElement(By.Id("operations-Books-get_api_v1_Books__id_")));
        op.FindElement(By.CssSelector(".opblock-summary")).Click();

        Wait.Until(d => op.FindElement(By.CssSelector(".try-out__btn"))).Click();
        var idInput = Wait.Until(d => op.FindElement(By.CssSelector("input[placeholder='id']")));
        idInput.SendKeys("1");
        op.FindElement(By.CssSelector(".execute")).Click();

        var status = Wait.Until(d => op.FindElement(By.CssSelector(".live-responses-table .response-col_status")));
        Assert.That(status.Text.Trim(), Is.EqualTo("200"));
        var body = op.FindElement(By.CssSelector(".live-responses-table .response-col_description pre")).Text;
        Assert.That(body, Does.Contain("\"title\": \"Book 1\""));
    }
}
