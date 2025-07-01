using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Education;
using Service.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace Service.Controllers
{
    [ApiController]
    public class EducationController([FromServices] ExperienceContext ctx) : Controller
    {

        private readonly ExperienceContext _context = ctx;

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/education")]
        public async Task<IActionResult> CreateEducation([FromBody] EducationWrite dto)
        {
            Education dbo = new()
            {
                School = dto.School,
                Degree = dto.Degree,
                Field = dto.Field,
                StartDate = DateOnly.Parse(dto.StartDate),
                EndDate = dto.EndDate != null ? DateOnly.Parse(dto.EndDate) : null,
                Description = dto.Description
            };
            _context.Education.Add(dbo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("/experience/education/{id}")]
        public async Task<IActionResult> ReadEducationById(int id)
        {
            Education? dbo = (await _context.Education.ToListAsync()).Find(dbo => dbo.Id == id);
            if (dbo == null) return NotFound($"No such education with ID {id}");

            EducationRead res = new()
            {
                Id = dbo.Id,
                School = dbo.School,
                Degree = dbo.Degree,
                Field = dbo.Field,
                StartDate = dbo.StartDate,
                EndDate = dbo.EndDate,
                Description = dbo.Description
            };
            return Ok(res);
        }

        [HttpGet("/experience/education")]
        public async Task<IActionResult> ReadAllEducation()
        {
            List<Education> dbo = await _context.Education.ToListAsync();

            IEnumerable<EducationRead> res = dbo.ConvertAll(dbo => new EducationRead()
                { 
                    Id = dbo.Id,
                    School = dbo.School,
                    Degree = dbo.Degree,
                    Field = dbo.Field,
                    StartDate = dbo.StartDate,
                    EndDate = dbo.EndDate,
                    Description = dbo.Description
                }
            );
            return Ok(res);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/education/{id}")]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] EducationWrite dto)
        {
            Education? dbo = (await _context.Education.ToListAsync()).Find(dbo => dbo.Id == id);
            if (dbo == null) return NotFound($"No such education with ID {id}");

            dbo.School = dto.School;
            dbo.Degree = dto.Degree;
            dbo.Field = dto.Field;
            dbo.StartDate = DateOnly.Parse(dto.StartDate);
            dbo.EndDate = dto.EndDate == null ? null : DateOnly.Parse(dto.EndDate);
            dbo.Description = dto.Description;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/education/{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            if (!_context.Education.Any(dbo => dbo.Id == id)) return NotFound($"No such education with ID {id}");

            await _context.Education.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
