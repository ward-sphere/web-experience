using Microsoft.AspNetCore.Mvc;
using Service.Infrastructure;

namespace Service.Controllers
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

            throw new ValidationFailedException()
            {
                Result = BadRequest(errorMessage)
            };
        }

        [HttpGet("/")]
        [HttpGet("/health")]
        public async Task<IActionResult> CheckHealth()
        {
            try
            {
                await CheckDatabaseConnection();
            }
            catch (ValidationFailedException e)
            {
                return e.Result;
            }

            return Ok();
        }
    }
}