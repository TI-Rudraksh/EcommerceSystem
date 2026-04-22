using EcommerceSystem.Models;

namespace EcommerceSystem.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{

    Task<IReadOnlyList<Order>> GetRecentOrdersAsync(int days);

    Task<Order?> GetOrderWithItemsAsync(int orderId);
}