using Microsoft.AspNetCore.Mvc;
using Service.Infrastructure;

namespace Service.Model
{
    public class HealthController([FromServices] ExperienceContext ctx) : Controller
    {

        private readonly ExperienceContext _context = ctx;

        private void CheckDatabaseConnection()
        {
            if (_context.Database.CanConnect()) return;

            string errorMessage = "Could not connect to database - check the"
                + " AIDSBOOGER__PORTFOLIO__EXPERIENCE__CONNSTR environment"
                + " variable and that database running";

            throw new CheckFailedException(Results.Problem(errorMessage));
        }

        [HttpGet("/")]
        [HttpGet("/health")]
        public IResult CheckHealth()
        {
            try
            {
                CheckDatabaseConnection();
            }
            catch (CheckFailedException e)
            {
                return e.Result;
            }

            return Results.Ok();
        }

        private class CheckFailedException(IResult result) : Exception
        {
            public IResult Result { get; set; } = result;

        }
    }
}