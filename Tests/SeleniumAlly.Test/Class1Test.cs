namespace SeleniumAlly.Test;

using SeleniumAlly;
using Xunit;

public class Class1Test
{
    [Fact]
    public void Given_When_Then()
    {
        var class1 = new WebDriverAutomation();

        Assert.NotNull(class1);
    }
}
