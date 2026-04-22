using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class DeleteProductsSpec: Specification<Product>
{
    public DeleteProductsSpec()
    {
        ApplyIgnoreQueryFilters();

        AddCriteria(p => p.IsDeleted);
    }
}