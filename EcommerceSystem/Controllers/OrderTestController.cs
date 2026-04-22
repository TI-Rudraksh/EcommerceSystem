using EcommerceSystem.Services;
using Microsoft.AspNetCore.Mvc;

public class OrderTestController : Controller
{
    private readonly OrderService _service;

    public OrderTestController(OrderService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Create()
    {
        var customerId = Guid.Parse("6de0bdd0-5bd9-42cc-9cd9-ef7fe4e663f7");

        var order = await _service.CreateOrderAsync(
            customerId,
            new()
            {
                new() { ProductId = 6, Quantity = 2 },
                new() { ProductId = 8, Quantity = 1 }
            });

        return Json(order);
    }
}