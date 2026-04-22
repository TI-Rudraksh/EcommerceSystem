using EcommerceSystem.Services.Interface;
using EcommerceSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EcommerceSystem.Data;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")
        );

        // Dummy tenant for migrations
        ITenantService tenantService =
            new DesignTimeTenantService();

        return new AppDbContext(
            optionsBuilder.Options,
            tenantService
        );
    }
}