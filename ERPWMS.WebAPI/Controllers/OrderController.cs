using ERPWMS.Domain.Entities;
using ERPWMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll([FromQuery] string type = "Sales")
        {
            return await _context.Orders.Where(o => o.Type == type).OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            order.CreatedTime = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPost("{id}/process")]
        public async Task<IActionResult> Process(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = "Completed";
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Order order)
        {
            if (id != order.Id) return BadRequest();
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Orders.AnyAsync(e => e.Id == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }
    }
}
