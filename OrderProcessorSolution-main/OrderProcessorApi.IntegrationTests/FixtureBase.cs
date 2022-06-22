
using Microsoft.Extensions.DependencyInjection;
namespace OrderProcessorApi.IntegrationTests;

public class FixtureBase : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null;


    public async Task DisposeAsync()
    {
        await AlbaHost.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        AlbaHost = await Alba.AlbaHost.For<Program>(builder =>
        {

            builder.ConfigureServices((context, services) =>
            {
                BuildServices(services);
            });
        });
    }

    protected virtual void BuildServices(IServiceCollection builder)
    {

    }

    
}
