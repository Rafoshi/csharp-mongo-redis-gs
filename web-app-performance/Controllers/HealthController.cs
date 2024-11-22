 using Microsoft.AspNetCore.Mvc;

namespace web_app_performance.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : Controller
    {
        [HttpGet(Name = "Health")]
        public IActionResult Health()
        {
            return Ok("Serviço online");
        }
    }
}
