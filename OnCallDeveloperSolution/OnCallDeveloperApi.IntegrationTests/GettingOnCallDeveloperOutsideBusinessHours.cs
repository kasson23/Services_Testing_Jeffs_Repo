
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnCallDeveloperApi.Adapters;

namespace OnCallDeveloperApi.IntegrationTests;

public class GettingOnCallDeveloperOutsideBusinessHours : IClassFixture<AfterBusinessHoursFixture>
{

    private readonly IAlbaHost _host;

    public GettingOnCallDeveloperOutsideBusinessHours(AfterBusinessHoursFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task GettingTheOncallDeveloperAsync()
    {
        var result = await _host.Scenario(api =>
        {
            // first I want to do a get request to the resource
            api.Get.Url("/"); // what host? what port? who cares!
            api.StatusCodeShouldBeOk();
            // I want to check the status code.
        });


        var returnedDeveloper = result.ReadAsJson<DeveloperResponse>();

        var expectedResponse = new DeveloperResponse
        {
            name = "Bill Smith",
            email = "bill@contractor.com",
            phone = "555-8888"
        };
        Assert.Equal(expectedResponse, returnedDeveloper);
    }
}

public class AfterBusinessHoursFixture : BaseFixture
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        var stubbedSystemTime = new Mock<ISystemTime>();
        stubbedSystemTime.Setup(c => c.GetCurrent()).Returns(new DateTime(2022, 6, 21, 18, 29, 00));

        services.AddSingleton<ISystemTime>(_ => stubbedSystemTime.Object);
    }
}