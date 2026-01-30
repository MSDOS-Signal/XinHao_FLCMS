using Microsoft.AspNetCore.Mvc;

namespace ERPWMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "Healthy", timestamp = DateTime.Now, version = "1.0.0" });
        }
    }
}
