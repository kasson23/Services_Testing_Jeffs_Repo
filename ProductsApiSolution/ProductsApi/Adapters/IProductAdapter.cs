using ProductsApi.Domain;
using ProductsApi.Models;

namespace ProductsApi.Adapters;

public interface IProductAdapter
{
    Task<IQueryable<Product>> GetProductsAsync();
    Task<Product> AddProductAsync(CreateProductRequest request);
}
