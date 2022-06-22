
using Moq;
using OnCallDeveloperApi.Domain;

namespace OnCallDeveloperApi.UnitTests;

public class GettingtheOnCallDeveloper
{
    [Fact]
    public void ReturnsCorrectBusinessHoursDeveloper()
    {
        var stubbedClock = new Mock<IProvideTheBusinessClock>();
        stubbedClock.Setup(c => c.IsBusinessHours()).Returns(true);
        var lookup = new DeveloperLookup(stubbedClock.Object);

        var dev = lookup.GetOnCallDeveloper();

        Assert.Equal("Sue Jones", dev.Name);
        // etc. etc.
    }

    [Fact]
    public void ReturnsCorrectOutsideBusinessHoursDeveloper()
    {

        var stubbedClock = new Mock<IProvideTheBusinessClock>();
        stubbedClock.Setup(c => c.IsBusinessHours()).Returns(false);
        var lookup = new DeveloperLookup(stubbedClock.Object);

        var dev = lookup.GetOnCallDeveloper();

        Assert.Equal("Bill Smith", dev.Name);
        // etc. etc.
    }
}
