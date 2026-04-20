using EcommerceSystem.Data;

namespace EcommerceSystem.Seeders;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DatabaseSeeder(
        AppDbContext context,
        IEnumerable<IDataSeeder> seeders)
    {
        _context = context;
        _seeders = seeders;
    }

    public async Task SeedAllAsync()
    {
        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            await seeder.SeedAsync(_context);
        }
    }
}