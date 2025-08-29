using ECommerceOrderManagement.Data;
using ECommerceOrderManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ApplicationDbContext _context) : ControllerBase
    {
        // Case kodunu basit tutmak için herhangi bir pattern kullanmadım.
        // DI olarak ApplicationDbContext'i Constructor yerine Constructor Injection şeklinde kullandım.
        // Proje EndPointleri Swagger ile kullanılabilecek şekidle ReadMe.txt dosyasında dökümante edildi.

        // Yeni Sipariş Ekleme
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            // Stok kontrolü
            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    return BadRequest($"Yeterli stok bulunamadı: {product?.Name ?? "Ürün"}");
                }

                // Stok düşürme
                product.Stock -= item.Quantity;
                _context.Products.Update(product);
            }

            order.OrderDate = DateTime.Now;
            order.Status = "Yeni";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // Siparişleri Listeleme
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        // Sipariş Detayını Getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // Sipariş Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
