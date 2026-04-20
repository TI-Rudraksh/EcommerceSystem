using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceBackend.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.ProductId).IsRequired();
        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.UnitPrice).HasColumnType("numeric(10,2)");
        builder.Property(oi => oi.TotalPrice).HasColumnType("numeric(12,2)");
        builder.Property(oi => oi.CreatedAt).HasDefaultValueSql("NOW()");
        builder.ToTable(t =>
            t.HasCheckConstraint(
                "CK_OrderItem_Quantity",
                "\"Quantity\" > 0"
            ));
    }
}