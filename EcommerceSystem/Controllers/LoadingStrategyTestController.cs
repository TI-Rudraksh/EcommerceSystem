using EcommerceSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Controllers;

public class LoadingStrategyTestController : Controller
{
    private readonly AppDbContext _context;

    public LoadingStrategyTestController(AppDbContext context)
    {
        _context = context;
    }

    // 1️⃣ EAGER LOADING
    public async Task<IActionResult> Eager()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

        return Json(products.Select(p => new
        {
            p.Name,
            Category = p.Category.Name
        }));
    }

    // 2️⃣ EXPLICIT LOADING
    public async Task<IActionResult> Explicit(int id = 10)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        await _context.Entry(product)
            .Reference(p => p.Category)
            .LoadAsync();

        return Json(new
        {
            product.Name,
            Category = product.Category.Name
        });
    }

    // 3️⃣ PROJECTION (BEST PRACTICE)
    public async Task<IActionResult> Projection()
    {
        var products = await _context.Products
            .Select(p => new
            {
                p.Name,
                p.Price,
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        return Json(products);
    }

    // // 4️⃣ SPLIT QUERY vs SINGLE QUERY
    // public async Task<IActionResult> SplitQuery()
    // {
    //     var orders = await _context.Orders
    //         .Include(o => o.OrderItems)
    //         .Include(o => o.PaymentMethod)
    //         .AsSplitQuery()
    //         .ToListAsync();
    //
    //     return Json(new
    //     {
    //         Count = orders.Count
    //     });
    // }
}