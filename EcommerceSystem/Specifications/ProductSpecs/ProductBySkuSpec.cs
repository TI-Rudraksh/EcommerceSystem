using EcommerceSystem.Models;
using EcommerceSystem.Specifications.Base;

namespace EcommerceSystem.Specifications.ProductSpecs;

public class ProductBySkuSpec : Specification<Product>
{
    public ProductBySkuSpec(string sku)
    {
        AddCriteria(p => p.SKU == sku);
    }
}