namespace SeleniumAlly;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

/// <summary>
/// WebDriverAutomation.
/// </summary>
public class WebDriverAutomation : IDisposable
{
    /// <Summary>
    /// A Selenium WebDriver.
    /// </Summary>
    private IWebDriver? webDriver;

    private readonly ChromeOptions options = new();

    /// <Summary>
    /// WebDriverAutomation Default Constructor.
    /// </Summary>
    public WebDriverAutomation()
    {
        this.options.AddArgument("--disable-notifications");
        this.options.AddArgument("--start-maximized");
        this.options.AddArgument("--disable-popups");
        this.options.AddArgument("--disable-gpu");
        this.options.AddArgument("--ignore-certificate-errors");
        this.options.AddArgument("--disable-extensions");
        this.options.AddArgument("--disable-dev-shm-usage");
        //options.AddArgument("--window-position=-32000,-32000");
        //options.AddArgument("headless");
        //Disable webdriver flags or you will be easily detectable
        this.options.AddArgument("--disable-blink-features");
        this.options.AddArgument("--disable-blink-features=AutomationControlled");
    }

    /// <Summary>
    /// Constructor that can accept the options.
    /// </Summary>
    /// <param name="options"></param>
    public WebDriverAutomation(ChromeOptions options) => this.options = options;

    /// <Summary>
    /// StartChromeDriver start chrome driver.
    /// </Summary>
    public void StartChromeDriver()
    {
        _ = new DriverManager().SetUpDriver(new ChromeConfig());
        this.webDriver = new ChromeDriver(this.options);
    }

    /// <Summary>
    /// SetImplicitWait.
    /// </Summary>
    public void SetImplicitWait(int time)
    {
        if (this.webDriver != null)
        {
            this.webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }
    }

    /// <Summary>
    /// Get Selenium Browser PageSource.
    /// </Summary>
    /// <returns>string or null</returns>
    public string? GetPageSource(string uRL)
    {
        if (string.IsNullOrWhiteSpace(uRL))
        {
            throw new ArgumentException($"'{nameof(uRL)}' cannot be null or whitespace.", nameof(uRL));
        }

        if (this.webDriver != null)
        {
            this.webDriver.Navigate()
                 .GoToUrl(uRL);
            var pagesource = this.webDriver.PageSource;
            return pagesource;
        }

        return null;
    }

    /// <Summary>
    /// Close the browser.
    /// </Summary>
    public void TearDown()
    {
        try
        {
            if (this.webDriver != null)
            {
                this.webDriver.Close();
                Console.WriteLine("Driver Closed Successfully");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error::" + ex.Message);
        }

        try
        {
            if (this.webDriver != null)
            {
                this.webDriver.Quit();

                Console.WriteLine("Driver Quit Successfully");
            }
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
    /// <returns>IWebElement.</returns>
    public IWebElement? GetElement(
        string elementTag,
                                   string locator)
    {
        var by = GetObj(locator, elementTag);

        if (this.IsElementPresent(by) && this.webDriver != null)
        {
            return this.webDriver.FindElement(by);
        }

        return null;
    }

    /// <summary>
    /// Check if IsElementPresent.
    /// </summary>
    /// <param name="by"></param>
    /// <returns>boolean.</returns>
    public bool IsElementPresent(By by)
    {
        try
        {
            _ = this.webDriver?.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    /// <summary>
    /// Check if IsElementPresent.
    /// </summary>
    public void Dispose() => this.TearDown();
}
