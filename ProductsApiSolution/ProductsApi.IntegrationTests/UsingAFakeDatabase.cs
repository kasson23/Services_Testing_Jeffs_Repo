

using Alba;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using ProductsApi.Domain;

namespace ProductsApi.IntegrationTests;

public class UsingAFakeDatabase : IClassFixture<FakeDbContext>
{
    private readonly IAlbaHost _host;
    public UsingAFakeDatabase( FakeDbContext context)
    {
        _host = context.AlbaHost;
    }

    [Fact]
    public async Task DoIt()
    {
        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/products");
            api.StatusCodeShouldBeOk();
        });

        var data = response.ReadAsText();
        Assert.Equal("tacos", data);
    }
}


public class FakeDbContext: FixtureBase {

    public override void ConfigureServices(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EntityFrameworkProductCatalog>));

        if(descriptor == null)
        {
            throw new Exception();
        }
        services.Remove(descriptor);

        services.AddDbContext<EntityFrameworkProductCatalog>(options =>
        {
            options.UseInMemoryDatabase("FakeDbContext");
        });

        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var scopedService = scope.ServiceProvider;
        var db = scopedService.GetRequiredService<EntityFrameworkProductCatalog>();
        db.Database.EnsureCreated();
    }
}