using ERPWMS.Domain.Entities;
using ERPWMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkOrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkOrder>>> GetAll()
        {
            return await _context.WorkOrders.AsNoTracking().OrderByDescending(w => w.CreatedTime).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkOrder workOrder)
        {
            workOrder.CreatedTime = DateTime.Now;
            _context.WorkOrders.Add(workOrder);
            
            // Link to APS: Create a corresponding Production Plan
            var plan = new ProductionPlan
            {
                PlanNo = $"PLN-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}",
                ProductCode = workOrder.ProductName, // Assuming ProductName maps to Code for simplicity or they are same
                Quantity = workOrder.PlanQty,
                CompletedQuantity = 0,
                StartDate = DateTime.Now.AddDays(1), // Plan for tomorrow
                EndDate = DateTime.Now.AddDays(7),
                Priority = "Normal",
                Status = "Planned",
                WorkCenter = "Assembly" // Default
            };
            _context.ProductionPlans.Add(plan);

            await _context.SaveChangesAsync();
            return Ok(workOrder);
        }

        [HttpPost("{id}/report")]
        public async Task<IActionResult> ReportProgress(Guid id)
        {
            var wo = await _context.WorkOrders.FindAsync(id);
            if (wo == null) return NotFound();

            wo.CompletedQty += (int)(wo.PlanQty * 0.1);
            if (wo.CompletedQty >= wo.PlanQty)
            {
                wo.CompletedQty = wo.PlanQty;
                wo.Status = "Completed";
                wo.EndTime = DateTime.Now;
            }
            else
            {
                wo.Status = "Running";
            }
            
            // Sync to APS: Update corresponding Production Plan
            ProductionPlan? plan = null;
            if (!string.IsNullOrEmpty(wo.PlanNo))
            {
                plan = await _context.ProductionPlans.FirstOrDefaultAsync(p => p.PlanNo == wo.PlanNo);
            }
            
            // Fallback: Try to find a plan that matches Product and Qty (heuristic)
            if (plan == null)
            {
                plan = await _context.ProductionPlans
                    .Where(p => p.ProductCode == wo.ProductName && p.Status != "Completed")
                    .OrderByDescending(p => p.CreatedTime)
                    .FirstOrDefaultAsync();
            }

            if (plan != null)
            {
                // Accumulate progress.
                // Calculate delta (we added 10%)
                int delta = (int)(wo.PlanQty * 0.1);
                plan.CompletedQuantity += delta;
                if (plan.CompletedQuantity > plan.Quantity) plan.CompletedQuantity = plan.Quantity;
                
                if (plan.CompletedQuantity >= plan.Quantity)
                {
                    plan.Status = "Completed";
                }
                else
                {
                    plan.Status = "InProgress";
                }
            }

            await _context.SaveChangesAsync();
            return Ok(wo);
        }

        [HttpPost("{id}/revert")]
        public async Task<IActionResult> RevertProgress(Guid id)
        {
            var wo = await _context.WorkOrders.FindAsync(id);
            if (wo == null) return NotFound();

            wo.CompletedQty -= (int)(wo.PlanQty * 0.1);
            if (wo.CompletedQty < 0) wo.CompletedQty = 0;

            if (wo.CompletedQty < wo.PlanQty)
            {
                wo.Status = "Running";
                wo.EndTime = null;
            }
            if (wo.CompletedQty == 0)
            {
                wo.Status = "Pending";
            }

            await _context.SaveChangesAsync();
            return Ok(wo);
        }
    }
}
