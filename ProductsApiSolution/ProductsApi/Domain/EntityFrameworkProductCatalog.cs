using Microsoft.EntityFrameworkCore;
using ProductsApi.Adapters;
using ProductsApi.Models;

namespace ProductsApi.Domain;

public class EntityFrameworkProductCatalog : DbContext, IProductAdapter
{

    public EntityFrameworkProductCatalog(DbContextOptions<EntityFrameworkProductCatalog> options) : base(options)
    {

    }
    public virtual DbSet<Product>? Products { get; set; } = null;

    public async Task<Product> AddProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Description = request.Description,
            Price = request.Price!.Value
        };

        Products!.Add(product);
        await SaveChangesAsync();
        return product;
    }

    public async Task<IQueryable<Product>> GetProductsAsync()
    {
        var products = await Products!.ToListAsync();

        return products.AsQueryable();
    }
}
