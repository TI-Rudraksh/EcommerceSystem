using System.Linq.Expressions;
using EcommerceSystem.Data;
using EcommerceSystem.Repositories.Interfaces;
using EcommerceSystem.Specifications.Evaluator;
using EcommerceSystem.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(object id)
        => await _dbSet.FindAsync(new object[] { id });
    
    public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet
        .AsNoTracking()
        .Where(predicate)
        .ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }
    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);

    public async Task<bool> ExistsAsync(
        Expression<Func<T, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null)
        => predicate == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(predicate);
    
    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }
    

    private IQueryable<T> ApplySpecification(
        ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(
            _context.Set<T>().AsQueryable(),
            spec
        );
    }
    
}