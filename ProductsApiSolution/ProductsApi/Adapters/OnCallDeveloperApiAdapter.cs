namespace ProductsApi.Adapters;

public class OnCallDeveloperApiAdapter : IOnCallDeveloperApiAdapter
{
    private readonly HttpClient _client;

    public OnCallDeveloperApiAdapter(HttpClient client)
    {
        _client = client;

    }


    public async Task<DeveloperResponse?> GetOnCallDeveloperAsync()
    {
        var response = await _client.GetAsync("/");

        response.EnsureSuccessStatusCode();

        var dev = await response.Content.ReadFromJsonAsync<DeveloperResponse>();
        return dev;
    }

}

public record DeveloperResponse
{
    public string name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string phone { get; set; } = string.Empty;
}
