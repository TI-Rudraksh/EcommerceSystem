using EcommerceSystem.Data;

namespace EcommerceSystem.Seeders;

public interface IDataSeeder
{
    Task SeedAsync(AppDbContext context);
    
    int Order { get; }
}