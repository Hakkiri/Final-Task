using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Page_objects.Pages;

public class MainPage
{
    private readonly IWebDriver driver;
    private static readonly By dashboardTitle = By.ClassName("app_logo");
    public IWebElement Title { get; private set; } = null!;
    

    public MainPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    public MainPage LocateElements()
    {
        Title = driver.FindElement(dashboardTitle);

        return this;
    }
}
