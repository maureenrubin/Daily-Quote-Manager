using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuoteManager.Api.Controllers.Quotes
{
    [Authorize (Roles = "DefaultUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {

         [HttpGet("testingconnection")]
        public async Task<ActionResult> Testing()
        {
            return Ok("Testing");
        }

    }
}
