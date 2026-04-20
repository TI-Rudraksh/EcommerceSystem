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
        Console.WriteLine("DatabaseSeeder started");

        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            Console.WriteLine($"Running {seeder.GetType().Name}");
            await seeder.SeedAsync(_context);
        }
    }
}