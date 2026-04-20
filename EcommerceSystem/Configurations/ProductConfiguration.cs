using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceSystem.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Price).HasColumnType("numeric(10, 2)");
        builder.Property(p => p.SKU).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CreatedAt).IsRequired().HasMaxLength(100);
        builder.Property(p => p.IsActive).HasDefaultValue(true);
        builder.HasIndex(p => p.SKU).IsUnique();
        builder.ToTable(t => t.HasCheckConstraint(    
            "CK_Product_Price",
            "\"Price\" >= 0"
            ));
        builder.Property(p => p.Stock).HasDefaultValue(0);
        // builder.HasData(
        //     new Product
        //     {
        //         Id = 1,
        //         Name = "Wireless Mouse",
        //         Price = 29.99m,
        //         Stock = 150,
        //         SKU = "ELEC-MOUSE-001",
        //         CategoryId = 1
        //     }
        // );
    }
}