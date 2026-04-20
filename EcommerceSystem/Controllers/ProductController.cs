using EcommerceSystem.Models;
using EcommerceSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.Controllers;

public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    // CREATE
    public async Task<IActionResult> Create()
    {
        var product = new Product()
        {
            Name = "Test Keyboard",
            Price = 1999,
            Stock = 50,
            SKU = "TEST-KEY-001",
            CategoryId = 4
        };

        await _productService.CreateAsync(product);

        return Json(new
        {
            message = "Product created successfully",
            productId = product.Id
        });
    }

    // READ
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound("Product not found");

        return Json(product);
    }

    // UPDATE
    public async Task<IActionResult> Update(int id = 6)
    {
        await _productService.UpdatePriceAsync(id, 2999);

        return Json(new
        {
            message = $"Product {id} updated successfully"
        });
    }

    // DELETE
    public async Task<IActionResult> Delete(int id = 1)
    {
        await _productService.DeleteAsync(id);

        return Json(new
        {
            message = $"Product {id} deleted successfully"
        });
    }
}