using EcommerceSystem.Data;
using EcommerceSystem.Models;

namespace EcommerceSystem.Repositories.Interfaces;

public interface IUnitofWork : IDisposable
{
    public IRepository<Product> Products { get;  }
    public IRepository<Category> Categories { get;  }
    public IOrderRepository Orders { get;  }
    public IRepository<Customer> Customers { get;  }
    public Task<int> SaveChangesAsync(CancellationToken ct = default);
    
}