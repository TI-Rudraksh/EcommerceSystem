using System.Linq.Expressions;
using EcommerceSystem.Specifications.Interfaces;

namespace EcommerceSystem.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(object id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);
    
    
}