using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Work;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    [EnableCors(PolicyName = "_allowFrontendOrigin")]
    public class WorkController([FromServices] ExperienceContext ctx) : Controller
    {
        private readonly ExperienceContext _context = ctx;

        private static readonly string[] ALLOWED_EMPLOYMENT_TYPE_INPUT
            = [ "Full-time", "Part-time", "Internship", "Contract" ];

        private static readonly int REQUIRED_COUNTRY_LENGTH = 3,
            REQUIRED_STATE_LENGTH = 2;

        private void ValidateEmploymentType(string employmentType)
        {
            if (ALLOWED_EMPLOYMENT_TYPE_INPUT.Contains(employmentType)) return;

            string errorMessage = $"Invalid employment type {employmentType}. " +
                    $"Must match one of: {String.Join(", ", ALLOWED_EMPLOYMENT_TYPE_INPUT)}";
            throw new ValidationFailedException()
            {
                Result = BadRequest(errorMessage)
            };
        }

        private void ValidateCountry(string country)
        {
            if (country.Length == REQUIRED_COUNTRY_LENGTH) return;

            string errorMessage = $"Invalid country code {country}. "
                    + $"Must have length {REQUIRED_COUNTRY_LENGTH}";
            throw new ValidationFailedException()
            {
                Result = BadRequest(errorMessage)
            };
        }

        private void ValidateState(string state)
        {
            if (state.Length == REQUIRED_STATE_LENGTH) return;

            string errorMessage = $"Invalid state {state}. "
                    + $"Must have length {REQUIRED_STATE_LENGTH}";
            throw new ValidationFailedException()
            {
                Result = BadRequest(errorMessage)
            };
        }

        private void ValidateLocation(Location loc)
        {
            ValidateCountry(loc.Country);
            ValidateState(loc.State);
        }

        private async Task ValidateWork(int id)
        {
            if (await _context.Work.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such Work with ID {id}")
            };
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/work")]
        public async Task<IActionResult> CreateWork([FromBody] WorkWrite dto)
        {
            try
            {
                ValidateEmploymentType(dto.EmploymentType);
                ValidateLocation(dto.Location);
            } 
            catch (ValidationFailedException e)
            {
                return e.Result;
            }
            

            Work dbo = new()
            {
                Title = dto.Title,
                EmploymentType = dto.EmploymentType,
                Organization = dto.Organization,
                StartDate = DateOnly.Parse(dto.StartDate),
                EndDate = dto.EndDate == null
                    ? null
                    : DateOnly.Parse(dto.EndDate),
                Location = $"{dto.Location.City}, {dto.Location.State}, {dto.Location.Country}",
                Description = dto.Description
            };

            await _context.Work.AddAsync(dbo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private static WorkRead Convert(Work dbo)
        {
            string[] locations = dbo.Location.Split(", ");
            return new WorkRead()
            {
                Id = dbo.Id,
                Title = dbo.Title,
                EmploymentType = dbo.EmploymentType,
                Organization = dbo.Organization,
                StartDate = dbo.StartDate,
                EndDate = dbo.EndDate,
                Location = new Location()
                {
                    City = locations[0],
                    State = locations[1],
                    Country = locations[2]
                },
                Description = dbo.Description
            };
        }

        [HttpGet("/experience/work/{id}")]
        public async Task<IActionResult> ReadWorkById(int id) {
            try { await ValidateWork(id); }
            catch (ValidationFailedException e) { return e.Result; }

            Work dbo = await _context.Work.FirstAsync(dbo => dbo.Id == id);
            return Ok(Convert(dbo));
        }

        [HttpGet("/experience/work")]
        public async Task<IActionResult> ReadWork()
        {
            List<Work> dbo = await _context.Work.ToListAsync();
            return Ok(dbo.ConvertAll(Convert));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/work/{id}")]
        public async Task<IActionResult> UpdateWork(int id, [FromBody] WorkWrite dto)
        {
            try
            {
                await ValidateWork(id);
                ValidateEmploymentType(dto.EmploymentType);
                ValidateLocation(dto.Location);
            }
            catch (ValidationFailedException e)
            {
                return e.Result;
            }

            Work dbo = await _context.Work.FirstAsync(dbo => dbo.Id == id);
            
            dbo.Title = dto.Title;
            dbo.EmploymentType = dto.EmploymentType;
            dbo.Organization = dto.Organization;
            dbo.StartDate = DateOnly.Parse(dto.StartDate);
            dbo.EndDate = dto.EndDate == null
                ? null
                : DateOnly.Parse(dto.EndDate);
            dbo.Location = $"{dto.Location.City}, {dto.Location.State}, {dto.Location.Country}";
            dbo.Description = dto.Description;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            try { await ValidateWork(id); }
            catch (ValidationFailedException e) { return e.Result; }

            await _context.WorkAchievement.Where(dbo => dbo.WorkId == id).ExecuteDeleteAsync();
            await _context.WorkSkill.Where(dbo => dbo.WorkId == id).ExecuteDeleteAsync();
            await _context.Work.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
