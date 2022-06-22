
using Moq;
using Moq.Protected;
using ProductsApi.Adapters;
using System.Text;
using System.Text.Json;

namespace ProductsApi.UnitTests;

public class OnCallDeveloperApiAdapterTests
{
    [Fact]
    public async Task MakesTheRequestProperly()
    {
        // Given (Setup)
        var developerResponse = new DeveloperResponse
        {
            name = "Ludwig",
            email = "ludwig@aol.com",
            phone = "555-1212"
        };
        var json = JsonSerializer.Serialize(developerResponse);

        string url = "http://tacobell.com";

        var response = new HttpResponseMessage();
        response.StatusCode = System.Net.HttpStatusCode.OK;
        response.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var client = new HttpClient(mockHandler.Object);
        client.BaseAddress = new Uri(url);

        var service = new OnCallDeveloperApiAdapter(client);
        // When 
        var developer = await service.GetOnCallDeveloperAsync();
        // Then 
        Assert.Equal("Ludwig", developer?.name);

        // Did it use an HTTP Get and the right URL
        mockHandler.Protected().Verify("SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri!.ToString().StartsWith(url)),
            ItExpr.IsAny<CancellationToken>());
    }
}
