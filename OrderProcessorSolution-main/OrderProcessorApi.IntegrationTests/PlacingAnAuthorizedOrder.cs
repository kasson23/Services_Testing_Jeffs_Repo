
using Alba.Security;
using OrderProcessorApi.Controllers;

namespace OrderProcessorApi.IntegrationTests;

public class PlacingAnAuthorizedOrder : IClassFixture<AuthenticatedAsJeffFixture>
{
    private readonly IAlbaHost _host;

    public PlacingAnAuthorizedOrder(AuthenticatedAsJeffFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task RequiresAuthorization()
    {
        var response = await _host.Scenario(api =>
        {
            var cart = new ShoppingCart(new string[] { "Chips", "Tortiallas" }, "Hurry I'm Hungy");

            api.Post.Json(cart).ToUrl("/my/orders");
            api.StatusCodeShouldBe(201);
        });

        var order = response.ReadAsJson<Order>();

        Assert.Equal("Jeff", order!.name);
        Assert.Equal("8398398", order!.user);
    }

}

public class NoClaimsGets403 : IClassFixture<AuthenticatedButNotClaims>
{
    private readonly IAlbaHost _host;

    public NoClaimsGets403(AuthenticatedButNotClaims fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task TryWithNoClaimsFails()
    {
        var response = await _host.Scenario(api =>
        {
            var cart = new ShoppingCart(new string[] { "Chips", "Tortiallas" }, "Hurry I'm Hungy");

            api.Post.Json(cart).ToUrl("/my/orders");
            api.StatusCodeShouldBe(403);
        });

        // And we don't return an order!
        // TODO: Make sure the order isn't actually places by stubbing whatever
        // your order processor service would be and having it either:
        // - Throw an exception on any call (this is what I would probably do)
        // - Or expose it from the fixture as a mock, and verify it was called Times.Never()

        var content = response.ReadAsText();
        Assert.Equal("", content);
      
    }
}

public class AuthenticatedAsJeffFixture : AuthFixtureBase
{
    protected override JwtSecurityStub GetJwtSecurityStub()
    {
        return base.GetJwtSecurityStub()
            .With("sub", "8398398")
            .With("preferred_username", "Jeff")
            .With("nickname", "Gonzo");
    }
}

public class AuthenticatedButNotClaims : AuthFixtureBase { }