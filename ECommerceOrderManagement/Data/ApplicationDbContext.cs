using ECommerceOrderManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrderManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Case Test ve kontrol süreçlerinin hızlanması adına basit Entity yapısı ve InMemoryDatabase kullandım.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        // Case de Product için stok işlemleri istenmiş fakat EndPoint beklenmediğinden default seed değer atamaları gerçekleştirdim.
        public void Seed()
        {
            if (!Products.Any())
            {
                Products.AddRange(
                    new Product { Name = "Laptop", Stock = 50, Price = 1200.00M },
                    new Product { Name = "Telefon", Stock = 100, Price = 700.00M },
                    new Product { Name = "Tablet", Stock = 30, Price = 400.00M }
                );

                SaveChanges();
            }
        }
    }
}