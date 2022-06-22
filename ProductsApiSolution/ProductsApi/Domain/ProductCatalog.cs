using ProductsApi.Adapters;
using ProductsApi.Models;
using System.Linq;
namespace ProductsApi.Domain;

public class ProductCatalog
{
    private readonly IProductAdapter _adapter;

    public ProductCatalog(IProductAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<CollectionResult<ProductSummaryItemResponse>> GetAllAsync()
    {
        IQueryable<Product> products = await _adapter.GetProductsAsync();

        var data = products.Select(p => new ProductSummaryItemResponse
        {
            Id = p.Id.ToString(),
            Description = p.Description,
            Price = p.Price
        }).ToList();

        return new CollectionResult<ProductSummaryItemResponse>
        {
            Data = data
        };

    }

    public async Task<ProductSummaryItemResponse> AddItemAsync(CreateProductRequest request)
    {
        Product product = await _adapter.AddProductAsync(request);

        return new ProductSummaryItemResponse
        {
            Id = product.Id.ToString(),
            Description = product.Description,
            Price = product.Price
        };
    }
}
