using Browser_factory;
using OpenQA.Selenium;
using Page_objects.Pages;
using Xunit;

namespace Tests;

public class Tests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly LoginPage loginpage;
    private readonly MainPage mainpage;

    public Tests()
    {
        driver = DriverFactory.SetDriver(BrowserType.Chrome);
        loginpage = new LoginPage(driver);
        mainpage = new MainPage(driver);
    }

    [Fact]
    public void LoginWithEmptyCredentials_ShowsUsernameIsRequiredError()
    {
        loginpage
            .Open()
            .LocateElements()
            .FillField(loginpage.UsernameField, "Hello-world")
            .FillField(loginpage.PasswordField, "1234")
            .ClearField(loginpage.PasswordField)
            .ClearField(loginpage.UsernameField)
            .ClickLoginbutton();

        var actual = loginpage.ErrorMessage.Text;

        Assert.Equal("Epic sadface: Username is required", actual);
    }

    [Fact]
    public void LoginWithEmptyPassword_ShowsPasswordIsRequiredError()
    {
        loginpage
            .Open()
            .LocateElements()
            .FillField(loginpage.UsernameField, "Hello-world")
            .FillField(loginpage.PasswordField, "1234")
            .ClearField(loginpage.PasswordField)
            .ClickLoginbutton();

        var actual = loginpage.ErrorMessage.Text;

        Assert.Equal("Epic sadface: Password is required", actual);
    }

    [Theory]
    [InlineData("standard_user", "secret_sauce")]
    //[InlineData("locked_out_user", "secret_sauce")]
    [InlineData("problem_user", "secret_sauce")]
    [InlineData("performance_glitch_user", "secret_sauce")]
    [InlineData("error_user", "secret_sauce")]
    [InlineData("visual_user", "secret_sauce")]
    public void LoginWithValidCredentials_ShowsTitleSwagLabs(string username, string password)
    {
        loginpage
            .Open()
            .LocateElements()
            .FillField(loginpage.UsernameField, username)
            .FillField(loginpage.PasswordField, password)
            .ClickLoginbutton();

        mainpage.LocateElements();

        var actual = mainpage.Title.Text;

        Assert.Equal("Swag Labs", actual);
    }

    public void Dispose()
    {
        Thread.Sleep(1000);
        DriverFactory.QuitDriver();
    }
}