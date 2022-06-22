
using Alba;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsApi.Adapters;

namespace ProductsApi.IntegrationTests;

public class GetProductsCatalogFailure : IClassFixture<ExplodingCatalogFixture>
{
    private readonly IAlbaHost _host;
    private readonly DeveloperResponse _expected;
    public GetProductsCatalogFailure(ExplodingCatalogFixture fixture)
    {
        _host = fixture.AlbaHost;
        _expected = fixture.StubbedResponse;
    }

    [Fact]
    public async Task ShouldCallOnCallDeveloperApiOnFailureOfCatalog()
    {
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/products");
            api.StatusCodeShouldBe(500);

        });

        var returned = response.ReadAsJson<ErrorResponseMessage>();
        Assert.NotNull(returned);

        Assert.Equal(500, returned?.StatusCode);
        Assert.Equal("That done blewed up!", returned?.Message);
        Assert.Equal(_expected.name, returned?.ForHelpContact?.Name);
        // etc. etc.
    }
}


public class ExplodingCatalogFixture : FixtureBase
{
    public DeveloperResponse StubbedResponse = new DeveloperResponse { 
        name = "Elvira", 
        email = "elvira@aol.com", 
        phone = "888-1212" 
    };
    public override void ConfigureServices(IServiceCollection services)
    {
        var stubbedCatalog = new Mock<IProductAdapter>();
        stubbedCatalog.Setup(x => x.GetProductsAsync()).ThrowsAsync(new InvalidDataException());

        services.AddScoped<IProductAdapter>(_ => stubbedCatalog.Object);


        var stubbedApi = new Mock<IOnCallDeveloperApiAdapter>();
        stubbedApi.Setup(a => a.GetOnCallDeveloperAsync()).ReturnsAsync(StubbedResponse);

        var configuredServiceDescriptor = services
            .SingleOrDefault(d => d.ServiceType == typeof(IOnCallDeveloperApiAdapter));

        if(configuredServiceDescriptor is null)
        {
            throw new Exception(); // that isn't registed in your program.cs
        } else
        {
            services.Remove(configuredServiceDescriptor); 
        }
        services.AddSingleton<IOnCallDeveloperApiAdapter>(_ => stubbedApi.Object);
    }
}

public record ErrorResponseMessage
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public HelpDeskInfo? ForHelpContact { get; set; }

}

public record HelpDeskInfo
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

//public record DeveloperResponse
//{
//    public string name { get; set; } = string.Empty;
//    public string email { get; set; } = string.Empty;
//    public string phone { get; set; } = string.Empty;
//}
