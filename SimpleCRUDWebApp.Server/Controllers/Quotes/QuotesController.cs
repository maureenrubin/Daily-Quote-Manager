using DailyQuoteManager.Application.Contracts.Interfaces.Quote;
using DailyQuoteManager.Application.DTOs.Quote.Quotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuoteManager.Api.Controllers.Quotes
{
  //  [Authorize(Roles = "DefaultUser")]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController (IQuoteService quotesService) : ControllerBase
    {

        [HttpPost("create-quote")]
        public async Task<IActionResult> CreateQuote([FromBody] QuotesInputReqDto quotesDto)
        {
            var result = await quotesService.CreateQuotesAsync(quotesDto);
            return result == null ? NotFound() : Ok(result);
        }

    }
}
