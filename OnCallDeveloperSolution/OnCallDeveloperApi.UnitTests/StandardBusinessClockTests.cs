

using Moq;
using OnCallDeveloperApi.Adapters;
using OnCallDeveloperApi.Domain;
using System.Collections;

namespace OnCallDeveloperApi.UnitTests;

public class StandardBusinessClockTests
{
    [Theory]
    [InlineData(8,30,00)]
    [InlineData(11,59,18)]
    [InlineData(17,00,00)]

    public void DuringBusinessHoursClockReturnsTrue(int hour, int minute, int second)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(2022, 6, 21, hour, minute, second));
        IProvideTheBusinessClock clock = new StandardBusinessClock(stubbedClock.Object);

        Assert.True(clock.IsBusinessHours());
    }

    [Theory]
    [InlineData(8,29,59)]
    [InlineData(3,14,13)]
    [InlineData(17,01,01)] // ??
    public void AfterBusinessHoursClockReturnsFalse(int hour, int minute, int second)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(2022, 6, 21, hour, minute, second));
        IProvideTheBusinessClock clock = new StandardBusinessClock(stubbedClock.Object);

        Assert.False(clock.IsBusinessHours());
    }

    [Theory]
    [InlineData(6,25)]
    [InlineData(6,26)]
    public void ClosedOnWeekends(int month, int day)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(new DateTime(2022, month, day, 11, 00, 00));
        IProvideTheBusinessClock clock = new StandardBusinessClock(stubbedClock.Object);

        Assert.False(clock.IsBusinessHours());
    }

    [Theory]
    [ClassData(typeof(HolidaysData))]
    public void ClosedOnHolidays(DateTime holiday, string name)
    {
        var stubbedClock = new Mock<ISystemTime>();
        stubbedClock.Setup(c => c.GetCurrent()).Returns(holiday);
        IProvideTheBusinessClock clock = new StandardBusinessClock(stubbedClock.Object);

        var open = clock.IsBusinessHours(); // TODO: Found a Bug by Adding this!
        Assert.False(open);
    }

    [Fact(Skip = "Homework")]
    public void ConvertsToEasternTime()
    {
       // See "Nodatime" Nuget package. https://nodatime.org
    }
}

public class HolidaysData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new DateTime(2022, 12, 25), "christmas" };
        yield return new object[] { new DateTime(2022, 7, 4), "indepdence day" };
        
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

