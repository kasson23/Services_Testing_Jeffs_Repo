

using Alba;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsApi.Adapters;
using ProductsApi.Domain;
using ProductsApi.Models;

namespace ProductsApi.IntegrationTests;

public class AddingAProduct : IClassFixture<AddingAProductFixture>
{

    private readonly IAlbaHost _host;
    private readonly AddingAProductFixture _fixture;
    public AddingAProduct(AddingAProductFixture fixture)
    {
        _fixture = fixture;
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task CanAddAProduct()
    {
        var response = await _host.Scenario(api =>
        {
            var requestData = _fixture.ModelToSend;
            api.Post.Json(requestData).ToUrl("/products");
            api.StatusCodeShouldBe(201); // lie!
        });

        var addedProduct = response.ReadAsJson<ProductSummaryItemResponse>(); // Todo: Don't use the API Models

        Assert.Equal("beer", addedProduct?.Description);

        //_fixture.MockedAdapter.Verify(x => x.AddProductAsync(It.Is<>)
    }


    [Fact]
    public async Task ValidationIsHookedUp()
    {
        var response = await _host.Scenario(api =>
        {
            var gratiutiouslyBadRequest = new CreateProductRequest { Description = null, Price = null };
            api.Post.Json(gratiutiouslyBadRequest).ToUrl("/products");
            api.StatusCodeShouldBe(400); // TRUTH!!
        });
    }
}


public class AddingAProductFixture: FixtureBase {

    public Mock<IProductAdapter> MockedAdapter;
    public CreateProductRequest ModelToSend = new CreateProductRequest { Description="beer", Price=12.99M};
    public Product ExpectedResponse = new Product { 
        Id = 42, 
        Description = "beer", 
        Price = 12.99M };
    public override void ConfigureServices(IServiceCollection services)
    {
        var stubbedProductCatalog = new Mock<IProductAdapter>();
        stubbedProductCatalog.Setup(a => a.AddProductAsync(It.IsAny<CreateProductRequest>())).ReturnsAsync(ExpectedResponse);

        services.AddScoped<IProductAdapter>(_ => stubbedProductCatalog.Object);
        MockedAdapter = stubbedProductCatalog;
    }
}

//public recod CreateProductModel(string? description, decimal? price);