using EcommerceSystem.Data;
using EcommerceSystem.Models.Base;

namespace EcommerceSystem.Models;

public class Product : BaseEntity, ITenantEntity
{
    public int Id { get; set; }

    public string TenantId { get; set; } = "";

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
    
    public string SKU { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; } 
    
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
    
    public ICollection<OrderItem> OrderItems { get; set; } 
    
    public ICollection<ProductTag> ProductTags { get; set; }
}