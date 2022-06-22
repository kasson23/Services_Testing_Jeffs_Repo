


using Alba.Security;
using Microsoft.Extensions.DependencyInjection;

namespace OrderProcessorApi.IntegrationTests;

public class AuthFixtureBase : IAsyncLifetime
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
        }, GetJwtSecurityStub());
    }

    protected virtual void BuildServices(IServiceCollection builder)
    {

    }

    protected virtual JwtSecurityStub GetJwtSecurityStub()
    {
        return new JwtSecurityStub();
    }

}
