using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace solticsongor_BooksFInd;

public class SeleniumHandler
{
    public IWebDriver? driver; //nincs chrome-om, és nem is akarom telepíteni

    public void Setup()
    {
        driver = new ChromeDriver();
    }

    public void Close()
    {
        driver?.Quit();
    }
}