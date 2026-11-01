using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using SeleniumExtras.WaitHelpers;

namespace solticsongor_SajatSite;

[TestClass]
public sealed class Test1
{
    private IWebDriver _driver;
    private WebDriverWait _wait;
    private string _baseUrl;
    private const string BravePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
    
    [TestInitialize]
    public void Setup()
    {
        var options = new ChromeOptions();
        if (File.Exists(BravePath))
        {
            options.BinaryLocation = BravePath;
        }

        options.AddArgument("--start-maximized");

        _driver = new ChromeDriver(options);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "../../../../../"));
        string htmlPath = Path.Combine(projectRoot, "orszagok", "orszagok.html");
        
        _baseUrl = new Uri(htmlPath).AbsoluteUri;
        _driver.Navigate().GoToUrl(_baseUrl);
    }

    [TestCleanup]
    public void Teardown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [TestMethod]
    public void TestLanguage()
    {
        var htmlElement = _driver.FindElement(By.TagName("html"));
        string lang = htmlElement.GetAttribute("lang");
        Assert.AreEqual("hu", lang, "The HTML language attribute is not 'hu'.");
    }

    [TestMethod]
    public void TestEncoding()
    {
        
        var metaCharset = _driver.FindElements(By.XPath("//meta[@charset='UTF-8']"));
        Assert.IsTrue(metaCharset.Count > 0, "Meta charset UTF-8 not found.");
    }

    [TestMethod]
    public void TestTitle()
    {
        string title = _driver.Title;
        Assert.IsNotNull(title, "Title is null.");
        Assert.AreEqual("Országok", title, "Title is not 'Országok'.");
    }

    [TestMethod]
    public void TestH1()
    {
        var h1Elements = _driver.FindElements(By.TagName("h1"));
        Assert.IsTrue(h1Elements.Count > 0, "No H1 tag found on the page.");
    }

    [TestMethod]
    public void TestH2()
    {
        var h2Elements = _driver.FindElements(By.TagName("h2"));
        Assert.IsTrue(h2Elements.Count > 0, "No H2 tag found on the page.");
    }
}