namespace SeleniumAlly.Test;

using SeleniumAlly;
using Xunit;

public class WebDriverAutomationTest
{
    [Fact]
    public void Given_When_Then()
    {
        var webDriverAutomation = new WebDriverAutomation();

        Assert.NotNull(webDriverAutomation);
    }
}
