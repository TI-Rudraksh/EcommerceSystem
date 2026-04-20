using EcommerceSystem.Data;
using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePriceAsync(int id, decimal price)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return;

        product.Price = price;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _context.Products.Remove(new Product { Id = id });

        await _context.SaveChangesAsync();
    }
    

}