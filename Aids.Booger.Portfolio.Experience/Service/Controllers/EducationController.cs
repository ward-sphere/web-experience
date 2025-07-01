using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Education;
using Service.Infrastructure;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Service.Controllers
{
    [ApiController]
    public class EducationController([FromServices] ExperienceContext ctx) : Controller
    {

        private readonly ExperienceContext _context = ctx;

        private async Task ValidateEducation(int id)
        {
            if (await _context.Education.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such Education with ID {id}")
            };
        }

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
            await _context.Education.AddAsync(dbo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private EducationRead Convert(Education dbo)
        {
            return new()
            {
                Id = dbo.Id,
                School = dbo.School,
                Degree = dbo.Degree,
                Field = dbo.Field,
                StartDate = dbo.StartDate,
                EndDate = dbo.EndDate,
                Description = dbo.Description
            };
        }

        [HttpGet("/experience/education/{id}")]
        public async Task<IActionResult> ReadEducationById(int id)
        {
            await ValidateEducation(id);

            Education dbo = await _context.Education.FirstAsync(dbo => dbo.Id == id);
            return Ok(Convert(dbo));
        }

        [HttpGet("/experience/education")]
        public async Task<IActionResult> ReadAllEducation()
        {
            List<Education> dbo = await _context.Education.ToListAsync();
            return Ok(dbo.ConvertAll(Convert));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/education/{id}")]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] EducationWrite dto)
        {
            await ValidateEducation(id);

            Education dbo = await _context.Education.FirstAsync(dbo => dbo.Id == id);
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
            await ValidateEducation(id);

            await _context.Education.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
