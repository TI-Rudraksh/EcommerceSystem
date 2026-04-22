using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class TopExpensiveProductsSpec : Specification<Product>
{
    public TopExpensiveProductsSpec(int count)
    {
        AddCriteria(p => p.IsActive);

        AddOrderByDescending(p => p.Price);

        ApplyPaging(0, count);
    }
}