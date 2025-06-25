using Microsoft.AspNetCore.Mvc;

namespace Service.Model
{
    public class HealthController : Controller
    {
        [HttpGet("/")]
        [HttpGet("/health")]
        public IResult CheckHealth()
        {
            return Results.Ok();
        }
    }
}