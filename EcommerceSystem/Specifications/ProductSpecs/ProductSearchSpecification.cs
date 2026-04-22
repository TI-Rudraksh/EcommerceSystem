using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class ProductSearchSpec : Specification<Product>
{
    public ProductSearchSpec(
        string? term,
        decimal? minPrice,
        decimal? maxPrice,
        int? categoryId,
        int page,
        int pageSize)
    {
        AddCriteria(p =>
            p.IsActive &&
            (string.IsNullOrEmpty(term) || p.Name.Contains(term)) &&
            (!minPrice.HasValue || p.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || p.Price <= maxPrice.Value) &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value)
        );

        AddInclude(p => p.Category);

        AddOrderByDescending(p => p.CreatedAt);

        ApplyPaging((page - 1) * pageSize, pageSize);
    }
}