using EcommerceSystem.Data;
using EcommerceSystem.Repositories.Implementations;
using EcommerceSystem.Repositories.Interfaces;
using EcommerceSystem.Seeders;
using EcommerceSystem.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information) // development only
        .EnableDetailedErrors() // development only
        .EnableSensitiveDataLogging() // development only
);
builder.Services.AddScoped<IDataSeeder, CategorySeeder>();
builder.Services.AddScoped<IDataSeeder, ProductSeeder>();

builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<ProductQueryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider
        .GetRequiredService<DatabaseSeeder>();

    await seeder.SeedAllAsync();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();