using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Education;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class EducationController([FromServices] ExperienceContext ctx) : Controller
    {

        private readonly ExperienceContext _context = ctx;

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/education")]
        public async Task CreateEducation([FromBody] EducationWrite dto)
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
            _context.Educations.Add(dbo);
            await _context.SaveChangesAsync();
        }

        [HttpGet("/experience/education/{id}")]
        public async Task<EducationRead> ReadEducationById(int id)
        {
            Education? dbo = (await _context.Educations.ToListAsync()).Find(dbo => dbo.Id == id) 
                ?? throw new HttpRequestException(message: $"No such education with ID {id}",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound);

            return new EducationRead()
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

        [HttpGet("/experience/education")]
        public async Task<IEnumerable<EducationRead>> ReadAllEducation()
        {
            List<Education> dbo = await _context.Educations.ToListAsync();

            return dbo.ConvertAll(dbo => new EducationRead()
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
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/education/{id}")]
        public async Task UpdateEducation(int id, [FromBody] EducationWrite dto)
        {
            Education? dbo = (await _context.Educations.ToListAsync()).Find(dbo => dbo.Id == id)
                ?? throw new HttpRequestException(message: $"No such education with ID {id}",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound);

            dbo.School = dto.School;
            dbo.Degree = dto.Degree;
            dbo.Field = dto.Field;
            dbo.StartDate = DateOnly.Parse(dto.StartDate);
            dbo.EndDate = dto.EndDate == null ? null : DateOnly.Parse(dto.EndDate);
            dbo.Description = dto.Description;

            await _context.SaveChangesAsync();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/education/{id}")]
        public async Task DeleteEducation(int id)
        {
            if (!_context.Educations.Any(dbo => dbo.Id == id))
            {
                throw new HttpRequestException(message: $"No such education with ID {id}",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound);
            }

            await _context.Educations.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
