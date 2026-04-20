using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceSystem.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.Property(o => o.TotalAmount).HasColumnType("numeric(12,2)");
        builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);
        builder.Property(o => o.ShippingAddress).IsRequired().HasMaxLength(500);
        builder.Property(o => o.PaymentMethod).IsRequired().HasMaxLength(50);
        builder.Property(o => o.IsPaid).HasDefaultValue(false);
        builder.Property(o => o.OrderDate).HasDefaultValueSql("NOW()");
        builder.Property(o => o.CreatedAt).HasDefaultValueSql("NOW()");
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Order_TotalAmount",
            "\"TotalAmount\" >= 0"
            ));
    }
}