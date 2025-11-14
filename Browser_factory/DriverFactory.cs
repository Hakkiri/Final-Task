using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Browser_factory;

public enum BrowserType
{
    Chrome,
    Edge,
    Firefox
}

public class DriverFactory
{
    private static ThreadLocal<IWebDriver?> driver = new ThreadLocal<IWebDriver?>();
    private DriverFactory(){}

    public static IWebDriver SetDriver(BrowserType type)
    {
        QuitDriver();

        if (driver.Value is null)
        {
            driver.Value = type switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Edge => new EdgeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                _ => throw new ArgumentException("invalid browser")
            };
        }

        driver.Value.Manage().Window.Maximize();
        driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        return driver.Value;
    }

    public static void QuitDriver()
    {
        driver.Value?.Quit();
        driver.Value?.Dispose();
        driver.Value = null;
    }
}