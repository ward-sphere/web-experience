using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Work;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class WorkController([FromServices] ExperienceContext ctx) : Controller
    {
        private readonly ExperienceContext _context = ctx;

        private static readonly string[] ALLOWED_EMPLOYMENT_TYPE_INPUT
            = [ "Full-time", "Part-time", "Internship", "Contract" ];

        private static readonly int REQUIRED_COUNTRY_LENGTH = 3,
            REQUIRED_STATE_LENGTH = 2;

        private static void ValidateEmploymentType(string employmentType)
        {
            if (ALLOWED_EMPLOYMENT_TYPE_INPUT.Contains(employmentType)) return;

            throw new HttpRequestException(
                message: $"Invalid employment type {employmentType}. "
                    + $"Must match one of: {String.Join(", ", ALLOWED_EMPLOYMENT_TYPE_INPUT)}",
                inner: null,
                statusCode: System.Net.HttpStatusCode.BadRequest
            );
        }

        private static void ValidateCountry(string country)
        {
            if (country.Length == REQUIRED_COUNTRY_LENGTH) return;

            throw new HttpRequestException(
                message: $"Invalid country code {country}. "
                    + $"Must have length {REQUIRED_COUNTRY_LENGTH}",
                inner: null,
                statusCode: System.Net.HttpStatusCode.BadRequest
            );
        }

        private static void ValidateState(string state)
        {
            if (state.Length == REQUIRED_STATE_LENGTH) return;

            throw new HttpRequestException(
                message: $"Invalid state {state}. "
                    + $"Must have length {REQUIRED_STATE_LENGTH}",
                inner: null,
                statusCode: System.Net.HttpStatusCode.BadRequest
            );
        }

        private static void ValidateLocation(Location loc)
        {
            ValidateCountry(loc.Country);
            ValidateState(loc.State);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/work")]
        public async Task CreateWork([FromBody] WorkWrite dto)
        {
            ValidateEmploymentType(dto.EmploymentType);
            ValidateLocation(dto.Location);

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
        }

        private static WorkRead DboToReadDto(Work dbo)
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

        private async Task<Work> FindDboById(int id)
        {
            Work? dbo = (await _context.Work.ToListAsync()).Find(x => x.Id == id)
                ?? throw new HttpRequestException(
                    message: $"No such work with ID {id}",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound
                );
            return dbo;
        }

        [HttpGet("/experience/work/{id}")]
        public async Task<WorkRead> ReadWorkById(int id) { return DboToReadDto(await FindDboById(id)); }

        [HttpGet("/experience/work")]
        public async Task<IEnumerable<WorkRead>> ReadWork()
        {
            List<Work> dbo = await _context.Work.ToListAsync();
            return dbo.ConvertAll(DboToReadDto);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/work/{id}")]
        public async Task UpdateWork(int id, [FromBody] WorkWrite dto)
        {
            ValidateEmploymentType(dto.EmploymentType);
            ValidateLocation(dto.Location);

            Work dbo = await FindDboById(id);
            
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
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/{id}")]
        public async Task DeleteWork(int id)
        {
            await FindDboById(id);
            await _context.Work.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
    }
}
