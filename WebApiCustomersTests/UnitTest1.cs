using Xunit;

namespace WebApiCustomersTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var actual = 4;
        Assert.Equal(4,actual);
    }
}