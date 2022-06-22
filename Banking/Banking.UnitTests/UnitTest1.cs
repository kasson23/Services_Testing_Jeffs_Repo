

namespace Banking.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange (Given)
        int a = 10, b = 20;
        // Act (When)
        int answer = a + b; // SUT
        // Assert (Then)
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(2,2,4)]
    [InlineData(3,3,6)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        int answer = a + b;
        Assert.Equal(expected, answer);
    }

}