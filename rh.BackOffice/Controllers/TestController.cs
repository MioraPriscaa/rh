namespace rh.BackOffice.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace rh.BackOffice.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class TestController : ControllerBase
        {
            [HttpGet("ping")]
            public IActionResult Ping()
            {
                return Ok(new { message = "pong" });
            }
        }
    }

}
