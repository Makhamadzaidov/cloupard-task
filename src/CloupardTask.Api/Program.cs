using CloupardTask.Api.Commons.Helpers;
using CloupardTask.Api.Commons.Middlewares;
using CloupardTask.Api.DbContexts;
using CloupardTask.Api.Interfaces.Services;
using CloupardTask.Api.Mappers;
using CloupardTask.Api.Services;
using CloupardTask.Service.Interfaces;
using CloupardTask.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(cors =>
{
	cors.AddPolicy("CorsPolicy", accesses =>
		accesses.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

string connectionString = builder.Configuration.GetConnectionString("CloupardProductionDb");
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

/*builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });*/

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();
app.Run();