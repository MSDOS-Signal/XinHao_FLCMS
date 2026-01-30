using ERPWMS.Domain.Entities;
using ERPWMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetAll()
        {
            return await _context.Inventories.AsNoTracking().OrderByDescending(i => i.CreatedTime).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventory inventory)
        {
            inventory.CreatedTime = DateTime.Now;
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return Ok(inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Inventory inventory)
        {
            if (id != inventory.Id) return BadRequest();
            _context.Entry(inventory).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Inventories.AnyAsync(e => e.Id == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Inventories.FindAsync(id);
            if (item == null) return NotFound();
            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<object>> GetDashboardStats()
        {
            try
            {
                var totalQty = 0m;
                try { totalQty = await _context.Inventories.SumAsync(i => i.Quantity); } catch (Exception ex) { Console.WriteLine($"SumAsync Error: {ex.Message}"); }
                
                var value = totalQty * 50; // Demo logic

                var pendingOrders = 0;
                try { pendingOrders = await _context.Orders.CountAsync(o => o.Status == "Pending"); } catch {}
                
                var systemAlerts = 0;
                try { systemAlerts = await _context.Devices.CountAsync(d => d.Status == "Alarm"); } catch {}

                var categoryDist = new List<object>();
                try 
                {
                    var rawDist = await _context.Inventories
                        .GroupBy(i => i.WarehouseCode)
                        .Select(g => new { Name = g.Key, Value = g.Sum(i => i.Quantity) })
                        .ToListAsync();
                    categoryDist.AddRange(rawDist);
                }
                catch {}

                var supplyStats = new { OnTime = 0, Total = 0 };
                try
                {
                    // Calculate "OnTime" as shipments that are Delivered.
                    // In a real scenario, this would compare ActualArrival <= EstimatedArrival.
                    // For this demo, we consider any "Delivered" shipment as a successful on-time delivery for the KPI.
                    var shipmentCount = await _context.Shipments.CountAsync();
                    if (shipmentCount > 0)
                    {
                        supplyStats = new 
                        {
                            OnTime = await _context.Shipments.CountAsync(s => s.Status == "Delivered"),
                            Total = shipmentCount
                        };
                    }
                    else
                    {
                        // Fallback to Orders (SRM usually involves Purchase Orders)
                        var orderCount = await _context.Orders.CountAsync(o => o.Type == "Purchase");
                        if (orderCount > 0)
                        {
                            supplyStats = new 
                            {
                                OnTime = await _context.Orders.CountAsync(o => o.Type == "Purchase" && o.Status == "Completed"),
                                Total = orderCount
                            };
                        }
                    }
                }
                catch {}

                return new 
                { 
                    TotalQuantity = totalQty, 
                    TotalValue = value,
                    PendingOrders = pendingOrders,
                    SystemAlerts = systemAlerts,
                    CategoryDistribution = categoryDist,
                    SupplyPerformance = supplyStats
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard Error: {ex.Message}");
                // Return zeros instead of crashing
                return new 
                { 
                    TotalQuantity = 0, 
                    TotalValue = 0,
                    PendingOrders = 0,
                    SystemAlerts = 0,
                    CategoryDistribution = new List<object>(),
                    SupplyPerformance = new { OnTime = 0, Total = 0 }
                };
            }
        }
    }
}
