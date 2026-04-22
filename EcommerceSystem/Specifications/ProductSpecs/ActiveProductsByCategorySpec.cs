using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class ActiveProductsByCategorySpec : Specification<Product>
{
    public ActiveProductsByCategorySpec(int categoryId)
    {
        AddCriteria(p => p.IsActive && p.CategoryId == categoryId);

        AddInclude(p => p.Category);

        AddOrderBy(p => p.Name);
    }
}