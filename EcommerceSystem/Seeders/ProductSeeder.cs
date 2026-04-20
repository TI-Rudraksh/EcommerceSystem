using EcommerceSystem.Data;
using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Seeders;

public class ProductSeeder : IDataSeeder
{
    public int Order => 2;

    public async Task SeedAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync())
            return;

        var electronics = await context.Categories
            .FirstOrDefaultAsync(c => c.Slug == "electronics");
        
        if (electronics == null)
            return;

        context.Products.Add(
            new Product()
            {
                Name = "Wireless Mouse",
                Price = 29.99m,
                Stock = 150,
                SKU = "ELEC-MOUSE-001",
                CategoryId = electronics.Id
            }
        );

        await context.SaveChangesAsync();
    }
}