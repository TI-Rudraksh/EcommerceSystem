using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class RestoreProductSpec : Specification<Product>
{
    public RestoreProductSpec(int productId)
    {
        ApplyIgnoreQueryFilters();

        AddCriteria(p => p.Id == productId && p.IsDeleted);
    }

}