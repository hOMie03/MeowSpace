using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeowSpace.API.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Cool, Cool.");
        }
    }
}
