using EcommerceSystem.Models;
using EcommerceSystem.Models.DTOs;
using EcommerceSystem.Repositories.Interfaces;

namespace EcommerceSystem.Services;

public class OrderService
{
    private readonly IUnitofWork _uow;

    public OrderService(IUnitofWork uow)
    {
        _uow = uow;
    }

    public async Task<Order> CreateOrderAsync(
        Guid customerId,
        List<CreateOrderItemDto> items)
    {
        var customer =
            await _uow.Customers.GetByIdAsync(customerId)
            ?? throw new Exception("Customer not found");

        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            OrderItems = new List<OrderItem>()
        };

        foreach (var item in items)
        {
            var product =
                await _uow.Products.GetByIdAsync(item.ProductId)
                ?? throw new Exception(
                    $"Product {item.ProductId} not found");

            if (product.Stock < item.Quantity)
                throw new Exception(
                    $"Insufficient stock for {product.Name}");

            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });

            product.Stock -= item.Quantity;
        }

        await _uow.Orders.AddAsync(order);

        await _uow.SaveChangesAsync();

        return order;
    }
}