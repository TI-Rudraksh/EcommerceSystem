using EcommerceSystem.Models;
using EcommerceSystem.Services;
using Microsoft.AspNetCore.Mvc;

public class ProductTestController : Controller
{
    private readonly ProductQueryService _service;

    public ProductTestController(ProductQueryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Search()
    {
        var products = await _service.SearchProducts(
            name: "Mouse",
            minPrice: 10,
            maxPrice: 5000
        );

        return Json(products);
    }

    public async Task<IActionResult> Pagination()
    {
        var result = await _service.GetPagedProducts(1, 5);

        return Json(result);
    }

    public async Task<IActionResult> Dashboard()
    {
        var stats = await _service.GetDashboardStats();

        return Json(stats);
    }
    
    public async Task<IActionResult> TopProducts()
    {
        var products = await _service.GetTop5ExpensiveProductsPerCategory();

        return Json(products);
    }
    public async Task<IActionResult> ApplyDiscount(int categoryId = 4)
    {
        await _service.ApplyDiscountToCategory(categoryId);

        return Json(new
        {
            message = "15% discount applied successfully"
        });
    }
}