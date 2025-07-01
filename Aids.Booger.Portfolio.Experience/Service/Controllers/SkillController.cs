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
        public async Task<IActionResult> CreateSkill([FromBody] SkillWrite dto)
        {
            if (await _ctx.Skill.AnyAsync(dbo => dbo.Name == dto.Name))
                return Conflict($"Already exists Skill with name {dto.Name}");

            Skill dbo = new()
            {
                Name = dto.Name
            };

            await _ctx.Skill.AddAsync(dbo);
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("/experience/skill/{id}")]
        public async Task<IActionResult> ReadSkillById(int id)
        {
            if (!await _ctx.Skill.AnyAsync(dbo => dbo.Id == id)) return NotFound($"No such skill with ID {id}");

            Skill dbo = await _ctx.Skill.FirstAsync(dbo => dbo.Id == id);
            SkillRead res = new()
            {
                Id = dbo.Id,
                Name = dbo.Name
            };
            return Ok(res);
        }

        [HttpGet("/experience/skill")]
        public async Task<IActionResult> ReadSkills()
        {
            IEnumerable<SkillRead> res = (await _ctx.Skill.ToListAsync())
                .ConvertAll(
                    dbo => new SkillRead()
                    {
                        Id = dbo.Id,
                        Name = dbo.Name
                    }
                );
            return Ok(res);
        }
    }
}
