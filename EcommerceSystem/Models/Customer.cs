using EcommerceSystem.Models;
using EcommerceSystem.Models.Base;

namespace EcommerceSystem.Data;

public class Customer: BaseEntity, ITenantEntity
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = "";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
    public DateTime CreatetAt { get; set; } =  DateTime.UtcNow;
    public bool isActive { get; set; } = true;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}