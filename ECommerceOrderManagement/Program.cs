using ECommerceOrderManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ECommerceDb")); // EF Core InMemory kullanýldý. Tercihe göre SQLLite veya SQL Server kullanýlabilir case kodunu basit tutmaya çalýþtým.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E Commerce Order Management API",
        Version = "v1"
    });
});

var app = builder.Build();

// Case de Product için stok iþlemleri istenmiþ fakat EndPoint beklenmediðinden default seed deðer atamalarýný program baþlarken yapýyoruz.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Seed();  // Varsayýlan ürünleri ekleme metodu.
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E Commerce Order Management API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
