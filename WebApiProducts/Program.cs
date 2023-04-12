using WebApiProducts;
using Microsoft.EntityFrameworkCore;
using WebApiProducts.Models;
using WebApiProducts.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddDbContext<ProductsContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EcomSurConnection"))

);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductsContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
