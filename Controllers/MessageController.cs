using Microsoft.AspNetCore.Mvc;

namespace WebApplicationPR.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet("message")]
        public IActionResult GetMessage()
        {
            return Ok("Привет! Это сообщение из контроллера.");
        }
    }
}