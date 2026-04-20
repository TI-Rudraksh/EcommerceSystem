using EcommerceSystem.Data;
using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Services;

public class ProductQueryService
{
    private readonly AppDbContext _context;

    public ProductQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> SearchProducts(
        string? name = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? categoryId = null,
        bool? isActive = null)
    {
        IQueryable<Product> query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(p => p.Name.Contains(name));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (isActive.HasValue)
            query = query.Where(p => p.IsActive == isActive.Value);

        return await query
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
    
    public async Task<(List<Product> Items, int TotalCount)>
        GetPagedProducts(int page, int pageSize)
    {
        var query = _context.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
    public async Task<object> GetDashboardStats()
    {
        var totalProducts = await _context.Products.CountAsync();

        var avgPrice = await _context.Products
            .AverageAsync(p => p.Price);

        var topCategories = await _context.Products
            .GroupBy(p => p.Category.Name)
            .Select(g => new
            {
                Category = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(3)
            .ToListAsync();

        var mostExpensive = await _context.Products
            .OrderByDescending(p => p.Price)
            .Select(p => new { p.Name, p.Price })
            .FirstOrDefaultAsync();

        return new
        {
            totalProducts,
            avgPrice,
            topCategories,
            mostExpensive
        };
    }
    
    public async Task<List<TopProductDto>> GetTop5ExpensiveProductsPerCategory()
    {
        return await _context.Database
            .SqlQuery<TopProductDto>($"""
              WITH ranked AS (
                  SELECT "Id","Name","Price","CategoryId",
                         ROW_NUMBER() OVER (
                             PARTITION BY "CategoryId"
                             ORDER BY "Price" DESC
                         ) AS rn
                  FROM "Products"
                  WHERE "IsActive" = true
              )
              SELECT "Id","Name","Price","CategoryId"
              FROM ranked
              WHERE rn <= 5
          """)
            .ToListAsync();
    }
    public async Task ApplyDiscountToCategory(int categoryId)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"""
             UPDATE "Products"
             SET "Price" = "Price" * 0.85
             WHERE "CategoryId" = {categoryId}
         """);
    }
}