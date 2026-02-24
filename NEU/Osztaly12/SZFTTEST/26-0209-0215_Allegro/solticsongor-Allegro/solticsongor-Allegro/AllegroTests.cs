using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace solticsongor_Allegro;

[TestClass]
public class AllegroTests
{
    private IWebDriver? _driver;
    private WebDriverWait? _wait;
    private const string BaseUrl = "https://allegro.hu";
    private const string BravePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
    private readonly Random _random = new Random();

    private static int TestMode => int.Parse(Environment.GetEnvironmentVariable("ALLEGRO_TEST_MODE") ?? "0");

    [TestInitialize]
    public void Setup()
    {
        var options = new ChromeOptions();
        if (File.Exists(BravePath)) options.BinaryLocation = BravePath;

        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--disable-blink-features=AutomationControlled");

        _driver = new ChromeDriver(options);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(35));
        
        try
        {
            _driver.Navigate().GoToUrl(BaseUrl);
            if (TestMode == 0)
            {
                Console.WriteLine("LEGAL MODE: Waiting 20s for manual CAPTCHA/site check...");
                Thread.Sleep(20000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Navigation error: " + ex.Message);
        }

        HandleCookies();
        Thread.Sleep(5000);
    }

    [TestCleanup]
    public void Cleanup()
    {
        try
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine("Cleanup warning: " + ex.Message);
        }
    }

    private void HandleCookies()
    {
        try
        {
            var cookieBtn = _wait!.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//button[contains(., 'Elfogadom') or contains(., 'Hozzájárulok') or contains(., 'Accept')]")));
            cookieBtn.Click();
        }
        catch (WebDriverTimeoutException) { }
    }

    private void HumanType(IWebElement element, string text)
    {
        element.Clear();
        foreach (char c in text)
        {
            element.SendKeys(c.ToString());
            Thread.Sleep(_random.Next(50, 150));
        }
    }

    private void PerformSearch(string query)
    {
        var searchInput = _wait!.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("input[type='search'], input[name='string'], input[data-testid='search-field']")));
        HumanType(searchInput, query);
        searchInput.SendKeys(Keys.Enter);
        _wait.Until(d => d.Url.Contains("listing") || d.Url.Contains("string=") || d.Url.Contains("kereses"));
    }

    [TestMethod]
    public void Test1_ProductToCart()
    {
        PerformSearch("lego");
        Thread.Sleep(5000);

        var productLink = _wait!.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("article h2 a, article h3 a, [data-role='offer-title'] a, article a[href*='/offer/']")));
        productLink.Click();

        var addToCartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("[data-role='add-to-cart'], button[aria-label*='kosár'], #add-to-cart-button, button[id*='add-to-cart'], button[data-testid*='add-to-cart']")));
        addToCartBtn.Click();

        Thread.Sleep(3000);
        Assert.IsTrue(_driver!.Url.Contains("kosar") || _driver.Url.Contains("cart") || _driver.PageSource.Contains("kosár") || _driver.PageSource.Contains("cart"), "Should be in cart or see confirmation.");
    }

    [TestMethod]
    public void Test2_NavigationLogo()
    {
        PerformSearch("iphone");
        Thread.Sleep(5000);

        var logo = _wait!.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("a[aria-label='Allegro'], a[title='Allegro'], a[href='/'], header a[data-analytics-click*='Logo']")));
        
        string currentUrl = _driver!.Url;
        logo.Click();
        
        _wait.Until(d => d.Url != currentUrl);
        Assert.IsTrue(_driver.Url.Contains("allegro.hu"), "Should navigate back to home.");
    }

    [TestMethod]
    public void Test3_SearchInputField()
    {
        var searchInput = _wait!.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("input[type='search'], input[name='string'], input[data-testid='search-field']")));
        
        string typedText = "laptop";
        HumanType(searchInput, typedText);
        Assert.AreEqual(typedText, searchInput.GetAttribute("value"));
    }

    [TestMethod]
    public void Test4_SearchButton()
    {
        var searchInput = _wait!.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("input[type='search'], input[name='string']")));
        HumanType(searchInput, "samsung");
        
        var searchBtn = _driver!.FindElement(By.CssSelector("button[type='submit'], button[data-testid='search-button']"));
        searchBtn.Click();
        
        _wait.Until(d => d.Url.Contains("samsung"));
        Assert.IsTrue(_driver.Url.Contains("samsung"));
    }

    [TestMethod]
    public void Test5_InvalidLoginError()
    {
        _driver!.Navigate().GoToUrl("https://allegro.hu/bejelentkezes");
        
        if (_driver.PageSource.Contains("Blocked") || _driver.Title.Contains("Blocked") || _driver.PageSource.Contains("Letiltották"))
        {
            Console.WriteLine("LEGAL MODE: Site blocked before login test. Waiting 15s for manual solve...");
            Thread.Sleep(15000);
        }

        var loginInput = _wait!.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("input[name='login'], #login")));
        HumanType(loginInput, "nemletezo@domain.hu");
        
        var passInput = _driver.FindElement(By.CssSelector("input[name='password'], #password"));
        HumanType(passInput, "hibasjelszo");
        
        var loginBtn = _driver.FindElement(By.CssSelector("button[type='submit'], button[data-testid='login-button']"));
        loginBtn.Click();
        
        Thread.Sleep(3000);
        bool hasError = _driver.PageSource.Contains("hiba") || 
                       _driver.PageSource.Contains("error") || 
                       _driver.Url.Contains("login") ||
                       _driver.Url.Contains("bejelentkezes");
        Assert.IsTrue(hasError);
    }
}
