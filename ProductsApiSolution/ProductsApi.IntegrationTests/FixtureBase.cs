using Alba;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsApi.IntegrationTests;

public class FixtureBase : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null;
    public async Task DisposeAsync()
    {
        await AlbaHost.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        AlbaHost = await Alba.AlbaHost.For<ProductsApi.Program>(builder =>
        {
            builder.ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            });
        });
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
       
    }
}
