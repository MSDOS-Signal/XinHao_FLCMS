using ERPWMS.Domain.Entities;
using ERPWMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPWMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeviceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Device>>> GetAll()
        {
            return await _context.Devices.AsNoTracking().ToListAsync();
        }

        [HttpPost("{id}/toggle")]
        public async Task<IActionResult> Toggle(Guid id, [FromQuery] string status)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            device.Status = status;
            await _context.SaveChangesAsync();
            return Ok(device);
        }

        [HttpPost("{id}/reset")]
        public async Task<IActionResult> ResetAlarm(Guid id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null) return NotFound();

            device.Status = "Stopped";
            device.LastAlarmMessage = "";
            await _context.SaveChangesAsync();
            return Ok(device);
        }
    }
}
