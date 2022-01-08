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
    public IWebDriver? WebDriver = null;

    private ChromeOptions options = new();

    /// <Summary>
    /// WebDriverAutomation Default Constructor
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

    private void StartChromeDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        this.WebDriver = new ChromeDriver(this.options);
        
    }

    private void SetImplicitWait(int time)
    {
        if(this.WebDriver != null)
        {
            this.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }
        
    }
    ///<Summary>
    /// Get Selenium Browser PageSource
    ///</Summary>
    public string? GetPageSource(string uRL)
    {
        if(this.WebDriver != null)
        {
            this.WebDriver.Navigate()
                 .GoToUrl(uRL);
            var pagesource = this.WebDriver.PageSource;
            return pagesource;
        }
        return null;

        
    }
    ///<Summary>
    /// Close the browser
    ///</Summary>
    public void TearDown()
    {
        try
        {
            if (this.WebDriver != null)
            {
                this.WebDriver.Close();
                Console.WriteLine("Driver Closed Successfully");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error::" + ex.Message);
        }

        try
        {
            if (this.WebDriver != null)
            {
                this.WebDriver.Quit();

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
    public IWebElement? GetElement(string elementTag, string locator)
    {
        var _by = GetObj(locator, elementTag);

        if (this.IsElementPresent(_by))
        {
            if (this.WebDriver != null)
            {
                return this.WebDriver.FindElement(_by);
            }
        }

        return null;
    }
    private bool IsElementPresent(By by)
    {
        try
        {
            if (this.WebDriver != null)
            {
                this.WebDriver.FindElement(by);
            }
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
