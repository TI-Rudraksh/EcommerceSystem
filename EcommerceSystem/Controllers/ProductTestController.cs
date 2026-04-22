using EcommerceSystem.Models;
using EcommerceSystem.Services;
using Microsoft.AspNetCore.Mvc;

public class ProductTestController : Controller
{
    private readonly ProductQueryService _service;
    private readonly ProductService _service2;

    public ProductTestController(ProductQueryService service, ProductService service2)
    {
        _service = service;
        _service2 = service2;
    }

    public async Task<IActionResult> Search()
    {
        var products = await _service.SearchProducts(
            name: "Keyboard",
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
    
    //Using Specifications 
    public async Task<IActionResult> ActiveProducts(int categoryId)
    {
        var products = await _service2.GetActiveProductsByCategoryAsync(categoryId);
        return Json(products);
    }

    // public async Task<IActionResult> GetProductsBySku(string sku)
    // {
    //     var products = await _service2.GetBySkuAsync(sku);
    //     return Json(products);
    // }
}