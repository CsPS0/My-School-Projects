using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace solticsongor_DemoBlaze;

[TestClass]
public sealed class Test1
{
    private IWebDriver _driver;
    private WebDriverWait _wait;
    private const string BaseUrl = "https://www.demoblaze.com/";
    private const string BravePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";

    [TestInitialize]
    public void Setup()
    {
        var options = new ChromeOptions();
        if (System.IO.File.Exists(BravePath))
        {
            options.BinaryLocation = BravePath;
        }

        options.AddArgument("--start-maximized");

        _driver = new ChromeDriver(options);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    [TestCleanup]
    public void Teardown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [TestMethod]
    public void Test1_HomePageTitle_ShouldBeStore()
    {
        // 1. Navigálás a főoldalra
        _driver.Navigate().GoToUrl(BaseUrl);

        // 2. Az oldal címének ellenőrzése
        Assert.AreEqual("STORE", _driver.Title, "The page title should be 'STORE'.");
    }

    [TestMethod]
    public void Test2_CategoryNavigation_Laptops_ShouldFilterProducts()
    {
        // 1. Navigálás a főoldalra
        _driver.Navigate().GoToUrl(BaseUrl);

        // 2. Kattintás a "Laptops" kategóriára
        // A link szövege pontosan "Laptops"
        var laptopsLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Laptops")));
        laptopsLink.Click();

        // 3. Ellenőrzés, hogy megjelenik-e egy laptop termék (pl. "Sony vaio i5")
        // Megvárjuk, amíg a lista frissül. A termékek linkek.
        var laptopProduct = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Sony vaio i5")));
        
        Assert.IsNotNull(laptopProduct, "Sony vaio i5 should be visible in the Laptops category.");
        Assert.IsTrue(laptopProduct.Displayed, "Laptop product should be displayed.");
    }

    [TestMethod]
    public void Test3_AddProductToCart_ShouldShowAlert()
    {
        // 1. Navigálás a főoldalra
        _driver.Navigate().GoToUrl(BaseUrl);

        // 2. Kattintás egy termékre (pl. "Samsung galaxy s6")
        var productLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Samsung galaxy s6")));
        productLink.Click();

        // 3. Kattintás a "Kosárba rakás" gombra
        // A gombnak van osztálya vagy onclick attribútuma, gyakran legkönnyebb "Add to cart" link szöveg alapján megtalálni 
        // vagy XPath //a[text()='Add to cart']
        var addToCartButton = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Add to cart")));
        addToCartButton.Click();

        // 4. Megvárjuk, amíg az értesítés (alert) megjelenik
        var alert = _wait.Until(ExpectedConditions.AlertIsPresent());

        // 5. Az értesítés szövegének ellenőrzése
        string alertText = alert.Text;
        Assert.IsTrue(alertText.Contains("Product added"), $"Alert text was '{alertText}' but expected it to contain 'Product added'.");

        // 6. Az értesítés elfogadása
        alert.Accept();
    }

    [TestMethod]
    public void Test4_ContactModal_ShouldOpen()
    {
        // 1. Navigálás a főoldalra
        _driver.Navigate().GoToUrl(BaseUrl);

        // 2. Kattintás a "Contact" (Kapcsolat) menüpontra a navigációs sávban
        var contactLink = _wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Contact")));
        contactLink.Click();

        // 3. Megvárjuk, amíg láthatóvá válik
        // Az ablak címe általában ID-vel vagy osztállyal rendelkezik. 
        // Tipikus Bootstrap modális ablakok alapján a cím egy 'modal-title' osztályú elemen belül lehet az exampleModal-ban
        var modalTitle = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("exampleModalLabel")));

        // 4. Az ablak címének ellenőrzése
        Assert.AreEqual("New message", modalTitle.Text, "Contact modal title should be 'New message'.");
    }
}