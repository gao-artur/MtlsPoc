using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("server/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello from server!";
        }
    }
}
