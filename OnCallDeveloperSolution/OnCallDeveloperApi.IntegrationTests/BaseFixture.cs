using Microsoft.Extensions.DependencyInjection;

namespace OnCallDeveloperApi.IntegrationTests;

public class BaseFixture : IAsyncLifetime
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
                services.AddMvcCore();
                ConfigureServices(services);
            });
        });
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // Design Pattern! Template method.
    }
}
