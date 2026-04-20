using EcommerceSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Seeders;

public class CategorySeeder : IDataSeeder
{
    public int Order => 1;

    public async Task SeedAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;
        Console.WriteLine("CategorySeeder running");
        
        context.Categories.AddRange(
            new Category { Name = "Electronics", Slug = "electronics" },
            new Category { Name = "Clothing", Slug = "clothing" },
            new Category { Name = "Books", Slug = "books" }
        );

        await context.SaveChangesAsync();
    }
}