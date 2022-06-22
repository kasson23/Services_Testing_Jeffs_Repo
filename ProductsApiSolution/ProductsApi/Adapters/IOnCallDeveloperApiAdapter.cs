namespace ProductsApi.Adapters;

public interface IOnCallDeveloperApiAdapter
{
    Task<DeveloperResponse> GetOnCallDeveloperAsync();
}