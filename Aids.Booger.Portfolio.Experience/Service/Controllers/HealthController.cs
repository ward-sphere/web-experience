using Microsoft.AspNetCore.Mvc;
using Service.Infrastructure;

namespace Service.Model
{
    public class HealthController([FromServices] ExperienceContext ctx) : Controller
    {

        private readonly ExperienceContext _context = ctx;

        private async Task CheckDatabaseConnection()
        {
            if (await _context.Database.CanConnectAsync()) return;

            string errorMessage = "Could not connect to database - check the"
                + " AIDSBOOGER__PORTFOLIO__EXPERIENCE__CONNSTR environment"
                + " variable and that database running";

            throw new CheckFailedException(BadRequest(errorMessage));
        }

        [HttpGet("/")]
        [HttpGet("/health")]
        public async Task<IActionResult> CheckHealth()
        {
            try
            {
                await CheckDatabaseConnection();
            }
            catch (CheckFailedException e)
            {
                return e.Result;
            }

            return Ok();
        }

        private class CheckFailedException(IActionResult result) : Exception
        {
            public IActionResult Result { get; set; } = result;

        }
    }
}