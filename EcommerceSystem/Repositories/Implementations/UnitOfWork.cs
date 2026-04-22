using EcommerceSystem.Data;
using EcommerceSystem.Models;
using EcommerceSystem.Repositories.Interfaces;

namespace EcommerceSystem.Repositories.Implementations;

public class UnitOfWork : IUnitofWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Products = new Repository<Product>(context);

        Categories = new Repository<Category>(context);

        Orders = new OrderRepository(context);

        Customers = new Repository<Customer>(context);
    }

    public IRepository<Product> Products { get; }

    public IRepository<Category> Categories { get; }

    // public IRepository<Order> Orders { get; }

    public IRepository<Customer> Customers { get; }

    public IOrderRepository Orders { get; }



    public async Task<int> SaveChangesAsync(
        CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public void Dispose()
        => _context.Dispose();
}