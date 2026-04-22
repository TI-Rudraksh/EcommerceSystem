using System.Linq.Expressions;
using EcommerceSystem.Models;
using EcommerceSystem.Models.Base;
using EcommerceSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Data;

public class AppDbContext : DbContext
{
    private readonly string _tenantId;
    public AppDbContext(DbContextOptions<AppDbContext> options,ITenantService tenantService) : base(options)
    {
        _tenantId = tenantService.GetCurrentTenantId();
    }
    public DbSet<Product> Products =>  Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer -> Orders (1:M)
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);


        // Order -> OrderItems (1:M)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);


        // OrderItem -> Product (M:1)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // modelBuilder.Entity<Category>().HasData(
        //     new Category
        //     {
        //         Id = 1,
        //         Name = "Electronics",
        //         ParentCategoryId = null
        //     },
        //
        //     new Category
        //     {
        //         Id = 2,
        //         Name = "Laptops",
        //         ParentCategoryId = 1
        //     },
        //
        //     new Category
        //     {
        //         Id = 3,
        //         Name = "Gaming Laptops",
        //         ParentCategoryId = 2
        //     }
        // );
        
        modelBuilder.Entity<ProductTag>()
            .HasKey(pt => new { pt.ProductId, pt.TagId });


        modelBuilder.Entity<ProductTag>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId);


        modelBuilder.Entity<ProductTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProductTags)
            .HasForeignKey(pt => pt.TagId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var tenantProperty = Expression.Property(
                    parameter,
                    nameof(ITenantEntity.TenantId)
                );

                var tenantCondition = Expression.Equal(
                    tenantProperty,
                    Expression.Constant(_tenantId)
                );

                var lambda = Expression.Lambda(tenantCondition, parameter);

                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    break;
                    
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                    
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
            }
        }
        foreach (var entry in ChangeTracker.Entries<ITenantEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantId;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}