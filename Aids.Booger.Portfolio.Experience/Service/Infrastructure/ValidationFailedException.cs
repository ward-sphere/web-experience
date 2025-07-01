using Microsoft.AspNetCore.Mvc;

namespace Service.Infrastructure
{
    public class ValidationFailedException : Exception
    {
        public IActionResult Result { get; set; }
    }
}
