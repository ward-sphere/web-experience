using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Skill;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class SkillController(ExperienceContext context) : Controller
    {
        private readonly ExperienceContext _ctx = context;

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/skill")]
        public async Task CreateSkill([FromBody] SkillWrite dto)
        {
            Skill dbo = new()
            {
                Name = dto.Name
            };

            await _ctx.Skill.AddAsync(dbo);
            await _ctx.SaveChangesAsync();
        }

        [HttpGet("/experience/skill/{id}")]
        public async Task<SkillRead> ReadSkillById(int id)
        {
            if (!_ctx.Skill.Any(dbo => dbo.Id == id))
            {
                throw new HttpRequestException(
                    message: $"No such skill with ID {id}",
                    inner: null,
                    statusCode: System.Net.HttpStatusCode.NotFound
                );
            }

            Skill dbo = await _ctx.Skill.FirstAsync(dbo => dbo.Id == id);
            return new SkillRead()
            {
                Id = dbo.Id,
                Name = dbo.Name
            };
        }

        [HttpGet("/experience/skill")]
        public async Task<IEnumerable<SkillRead>> ReadSkills()
        {
            return (await _ctx.Skill.ToListAsync())
                .ConvertAll(
                    dbo => new SkillRead()
                    {
                        Id = dbo.Id,
                        Name = dbo.Name
                    }
                );
        }
    }
}
