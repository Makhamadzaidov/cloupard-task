using CloupardTask.Api.Commons.Helpers;
using CloupardTask.Api.DbContexts;
using CloupardTask.Api.Mappers;
using CloupardTask.DataAccess.Interfaces.Orders;
using CloupardTask.DataAccess.Repositories.Orders;
using CloupardTask.Service.Interfaces.Categories;
using CloupardTask.Service.Interfaces.Commons;
using CloupardTask.Service.Interfaces.Customers;
using CloupardTask.Service.Interfaces.Orders;
using CloupardTask.Service.Interfaces.Products;
using CloupardTask.Service.Services.Categories;
using CloupardTask.Service.Services.Commons;
using CloupardTask.Service.Services.Customers;
using CloupardTask.Service.Services.Orders;
using CloupardTask.Service.Services.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

string connectionString = builder.Configuration.GetConnectionString("CloupardProductionDb");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    app.UseHsts();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.MapControllers();
app.Run();
