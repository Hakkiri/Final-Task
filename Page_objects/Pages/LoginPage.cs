using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Page_objects.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly Actions actions;
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentException(nameof(driver));
        actions = new Actions(driver);
    }

    private const string URL = "https://www.saucedemo.com";
    private static readonly By usernamefield = By.Id("user-name");
    private static readonly By passwordfield = By.Id("password");
    private static readonly By loginbutton = By.Id("login-button");
    private static readonly By errormessage = By.CssSelector(".error-message-container");

    public double Seconds { get; private set; } = 0.5;
    public IWebElement UsernameField { get; private set; } = null!;
    public IWebElement PasswordField { get; private set; } = null!;
    private IWebElement LoginButton { get; set; } = null!;
    public IWebElement ErrorMessage { get; private set; } = null!;

    public LoginPage Open()
    {
        driver.Navigate().GoToUrl(URL);
        return this;
    }

    public LoginPage LocateElements()
    {
        UsernameField = driver.FindElement(usernamefield);
        PasswordField = driver.FindElement(passwordfield);
        LoginButton = driver.FindElement(loginbutton);
        ErrorMessage = driver.FindElement(errormessage);

        return this;
    }

    public LoginPage FillField(IWebElement field, string? str = null)
    {
        actions
            .Click(field)
            .Pause(TimeSpan.FromSeconds(Seconds))
            .SendKeys(str)
            .Perform();

        return this;
    }

    public LoginPage ClearField(IWebElement field)
    {
        actions
            .Click(field)
            .Click(field)
            .Click(field)
            .Pause(TimeSpan.FromSeconds(Seconds))
            .SendKeys(Keys.Backspace)
            .Perform();

        return this;
    }

    public LoginPage ClickLoginbutton()
    {
        LoginButton.Click();

        return this;
    }
}
