using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models;

public class ProductSummaryItemResponse
{
    public string Id { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}


public class CollectionResult<T>
{
    public List<T> Data { get; set; } = new();

}

public class CreateProductRequest
{
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal? Price { get; set; }
}