using EcommerceSystem.Models;
using EcommerceSystem.Models.DTOs;
using EcommerceSystem.Repositories.Interfaces;
using EcommerceSystem.Specifications.ProductSpecs;

namespace EcommerceSystem.Services;

public class ProductService
{
    private readonly IUnitofWork _uow;

    public ProductService(IUnitofWork uow)
    {
        _uow = uow;
    }

    public async Task<Product?> GetByIdAsync(int id)
        => await _uow.Products.GetByIdAsync(id);

    public async Task<Product> CreateAsync(Product product)
    {
        await _uow.Products.AddAsync(product);

        await _uow.SaveChangesAsync();

        return product;
    }
    
    public async Task UpdatePriceAsync(int id, decimal price)
    {
        var product = await _uow.Products.GetByIdAsync(id);

        if (product == null) return;

        product.Price = price;

        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product =
            await _uow.Products.GetByIdAsync(id);

        if (product == null)
            return;

        _uow.Products.Delete(product);

        await _uow.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<ProductResponseDto>> GetActiveProductsByCategoryAsync(int categoryId)
    {
        var spec =
            new ActiveProductsByCategorySpec(categoryId);

        var products =  await _uow.Products.ListAsync(spec);
        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CategoryName = p.Category.Name
        }).ToList();
    }
    public async Task<Product?> GetBySkuAsync(string sku)
    {
        var spec = new ProductBySkuSpec(sku);

        return await _uow.Products.FirstOrDefaultAsync(spec);
    }
    public async Task<IReadOnlyList<Product>> GetTopSellingProductsAsync(int count)
    {
        var spec = new TopExpensiveProductsSpec(count);

        return await _uow.Products.ListAsync(spec);
    }
    public async Task<IReadOnlyList<Product>> SearchProductsAsync(
        string? term,
        decimal? minPrice,
        decimal? maxPrice,
        int? categoryId,
        int page,
        int pageSize)
    {
        var spec = new ProductSearchSpec(
            term,
            minPrice,
            maxPrice,
            categoryId,
            page,
            pageSize
        );

        return await _uow.Products.ListAsync(spec);
    }
}