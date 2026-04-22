using EcommerceSystem.Models;
using EcommerceSystem.Models.Base;

namespace EcommerceSystem.Data;

public class Category : BaseEntity, ITenantEntity
{
    public int Id { get; set; }
    
    public string TenantId { get; set; } = "";
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string Slug { get; set; } = string.Empty;
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }

    public ICollection<Category> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}