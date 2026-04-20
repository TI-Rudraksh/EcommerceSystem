using EcommerceSystem.Data;
using EcommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceSystem.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Slug).IsRequired().HasMaxLength(150);
        // builder.HasData(
        //     new Category { Id = 1, Name = "Electronics", Slug = "electronics" },
        //     new Category { Id = 2, Name = "Clothing", Slug = "clothing" },
        //     new Category { Id = 3, Name = "Books", Slug = "books" },
        //     new Category { Id = 4, Name = "Sports", Slug = "sports" },
        //     new Category { Id = 5, Name = "Home & Garden", Slug = "home-garden" }
        // );
    }
}