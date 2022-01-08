namespace SeleniumAlly;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

/// <summary>
/// WebDriverAutomation.
/// </summary>
public class WebDriverAutomation
{
    /// <Summary>
    /// A Selenium WebDriver
    /// </Summary>
    public IWebDriver WebDriver { get; set; }
    /// <Summary>
    /// WebDriverAutomation Default Constructor
    /// </Summary>
    public WebDriverAutomation()
    {
        ChromeOptions options = new();

        options.AddArgument("--disable-notifications");
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-popups");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--ignore-certificate-errors");
        options.AddArgument("--disable-extensions");
        options.AddArgument("--disable-dev-shm-usage");
        //options.AddArgument("--window-position=-32000,-32000");
        //options.AddArgument("headless");
        //Disable webdriver flags or you will be easily detectable
        options.AddArgument("--disable-blink-features");
        options.AddArgument("--disable-blink-features=AutomationControlled");

        new DriverManager().SetUpDriver(new ChromeConfig());
        this.WebDriver = new ChromeDriver(options);

        this.SetImplicitWait(10);
    }

    private void SetImplicitWait(int time)
    {
        WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
    }
    ///<Summary>
    /// Get Selenium Browser PageSource
    ///</Summary>
    public string GetPageSource(string uRL)
    {
        this.WebDriver.Navigate()
                 .GoToUrl(uRL);
        var pagesource = this.WebDriver.PageSource;

        return pagesource;
    }
    ///<Summary>
    /// Close the browser
    ///</Summary>
    public void TearDown()
    {
        try
        {
            this.WebDriver.Close();
            Console.WriteLine("Driver Closed Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error::" + ex.Message);
        }

        try
        {
            this.WebDriver.Quit();

            Console.WriteLine("Driver Quit Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error::" + ex.Message);
        }
    }

    private static By GetObj(string locatorType, string selector)
    {
        Dictionary<string, By> map = new();
        map.Add("ID", By.Id(selector));

        map.Add("NAME", By.Name(selector));
        map.Add("XPATH", By.XPath(selector));
        map.Add("TAG", By.TagName(selector));
        map.Add("CLASS", By.ClassName(selector));
        map.Add("CSS", By.CssSelector(selector));
        map.Add("LINKTEXT", By.LinkText(selector));

        return map[locatorType];
    }
    /// <Summary>
    /// Get Element by ElementTag and Locator
    /// </Summary>
    public IWebElement? GetElement(string elementTag, string locator)
    {
        var _by = GetObj(locator, elementTag);

        if (this.IsElementPresent(_by))
        {
            return this.WebDriver.FindElement(_by);
        }

        return null;
    }
    private bool IsElementPresent(By by)
    {
        try
        {
            this.WebDriver.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
