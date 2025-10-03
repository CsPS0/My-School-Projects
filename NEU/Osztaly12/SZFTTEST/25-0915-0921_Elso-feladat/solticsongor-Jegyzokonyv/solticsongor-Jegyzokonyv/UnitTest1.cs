using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace solticsongor_Jegyzokonyv;

public class Tests
{
    private IWebDriver _webDriver = null!;
    
    [SetUp]
    public void Setup()
    {
        var downloadPath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\.."));
        var chromeOptions = new ChromeOptions
        {
            BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"
        };
        chromeOptions.AddUserProfilePreference("download.default_directory", downloadPath);
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

        _webDriver = new ChromeDriver(chromeOptions);
        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
                var screenshotDirectory = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\.."));
                var testName = TestContext.CurrentContext.Test.Name;
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var filePath = Path.Combine(screenshotDirectory, $"{testName}_{timestamp}.png");
                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved to: {filePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to take screenshot: {e.Message}");
            }
        }
        _webDriver.Dispose();
    }

    [Test]
    public void FirkaApp_Footer_ContainsSocialLinks()
    {
        _webDriver.Navigate().GoToUrl("https://firka.app/");
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(15));

        try
        {
            var footer = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("footer")));
            
            var links = footer.FindElements(By.TagName("a"));
            Assert.That(links, Is.Not.Empty, "No links found in the footer.");

            var expectedDomains = new List<string> { "bsky.app", "tiktok.com", /* "discord.com" */ "instagram.com", "yoursit.ee" };
            
            var socialLinkUrls = links
                .Select(l => l.GetAttribute("href"))
                .Where(href => !string.IsNullOrEmpty(href) && expectedDomains.Any(domain => href.Contains(domain)))
                .ToList();

            Console.WriteLine("Found social links on firka.app:");
            socialLinkUrls.ForEach(Console.WriteLine);

            Assert.That(socialLinkUrls.Count, Is.EqualTo(5), $"Expected to find 5 social links, but found {socialLinkUrls.Count}.");

            foreach (var domain in expectedDomains) Assert.That(socialLinkUrls.Any(url => url.Contains(domain)), Is.True, $"Social link for '{domain}' was not found in the footer.");
        }
        catch (Exception e)
        {
            Assert.Fail($"Test failed while trying to find footer social links. Error: {e.Message}");
        }
    }

    [Test]
    public void Moodle_Login_WithRandomCredentials_ShouldFail()
    {
        _webDriver.Navigate().GoToUrl("https://moodle.njszki.hu/login/index.php");

        var randomUsername = "user_" + Guid.NewGuid().ToString().Substring(0, 8);
        var randomPassword = "pass_" + Guid.NewGuid().ToString().Substring(0, 8);

        var usernameInput = _webDriver.FindElement(By.Id("username"));
        var passwordInput = _webDriver.FindElement(By.Id("password"));
        var loginButton = _webDriver.FindElement(By.Id("loginbtn"));

        usernameInput.SendKeys(randomUsername);
        passwordInput.SendKeys(randomPassword);
        loginButton.Click();

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        var errorDiv = wait.Until(d => d.FindElement(By.CssSelector(".alert.alert-danger")));

        Console.WriteLine($"Login attempt for user '{randomUsername}' failed with message: {errorDiv.Text}");

        Assert.That(errorDiv.Displayed, Is.True, "Login error message was not displayed.");
    }

    [Test]
    public void UnoSite_DownloadGame_ShouldSucceed()
    {
        var downloadDirectory = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\.."));
        var expectedFilePath = Path.Combine(downloadDirectory, "Console-UNO.zip");

        if (File.Exists(expectedFilePath)) File.Delete(expectedFilePath);

        _webDriver.Navigate().GoToUrl("https://csps0.github.io/Console-UNO/");
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));

        try
        {
            var downloadButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.btn[download]")));
            downloadButton.Click();
        }
        catch (Exception e)
        {
            Assert.Fail($"Failed to find or click the download button. Error: {e.Message}");
        }

        try
        {
            wait.Until(d => File.Exists(expectedFilePath));
        }
        catch (WebDriverTimeoutException)
        {
            var logFilePath = Path.Combine(downloadDirectory, "download-error.log");
            var errorMessage = $"[{DateTime.Now}] ERROR: Download of 'Console-UNO.zip' failed or did not complete in 30 seconds.";
            File.WriteAllText(logFilePath, errorMessage);
            Assert.Fail(errorMessage);
        }

        var fileInfo = new FileInfo(expectedFilePath);
        Assert.That(fileInfo.Exists, Is.True, "Downloaded file was not found.");
        Assert.That(fileInfo.Length, Is.GreaterThan(0), "Downloaded file is empty.");
    }

    [Test]
    public void EmuOs_LaunchQuake_ShouldDisplayGameWindow()
    {
        _webDriver.Navigate().GoToUrl("https://emupedia.net/beta/emuos/");
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(120));

        try
        {
            var osChoices = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("box-content")));
            Assert.That(osChoices, Is.Not.Empty, "OS choices were not found.");
            
            var random = new Random();
            var randomChoice = osChoices[random.Next(osChoices.Count)];
            Console.WriteLine($"Randomly selected OS: {randomChoice.Text}");
            randomChoice.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("desktop-icons")));

            var quakeIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@class, 'desktop-icon') and .//span[text()='Quake']]")));
            
            Actions actions = new Actions(_webDriver);
            actions.DoubleClick(quakeIcon).Perform();

            var quakeWindow = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'window') and .//div[contains(@class, 'title-bar-text') and contains(text(), 'Quake')]]")));

            Assert.That(quakeWindow.Displayed, Is.True, "Quake game window did not appear.");
        }
        catch (Exception e)
        {
            Assert.Fail($"Test failed during EmuOS Quake launch sequence. Error: {e.Message}");
        }
    }
}