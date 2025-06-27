using Microsoft.AspNetCore.Mvc;
using Service.Dto.Education;

namespace Service.Controllers
{
    public class EducationController : Controller
    {
        [HttpPost("/experience/education")]
        public async Task CreateEducation([FromBody] EducationWrite dto)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/experience/education/{id}")]
        public async Task<EducationRead> ReadEducationById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/experience/education")]
        public async Task<IEnumerable<EducationRead>> ReadAllEducation()
        {
            throw new NotImplementedException();
        }

        [HttpPut("/experience/education/{id}")]
        public async Task UpdateEducation(int id, [FromBody] EducationWrite dto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("/experience/education/{id}")]
        public async Task DeleteEducation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
