using EcommerceSystem.Models;

namespace EcommerceSystem.Data;

public class Customer
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
    
    public DateTime CreatetAt { get; set; } =  DateTime.UtcNow;
    
    public bool isActive { get; set; } = true;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}