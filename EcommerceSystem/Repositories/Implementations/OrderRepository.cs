using EcommerceSystem.Data;
using EcommerceSystem.Models;
using EcommerceSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Repositories.Implementations;

public class OrderRepository
    : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IReadOnlyList<Order>>
        GetRecentOrdersAsync(int days)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);

        return await _dbSet
            .Where(o => o.OrderDate >= cutoffDate)
            .Include(o => o.Customer)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?>
        GetOrderWithItemsAsync(int orderId)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}