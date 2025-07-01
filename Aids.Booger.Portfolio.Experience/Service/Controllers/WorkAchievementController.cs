using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.Work;
using Service.Dto.WorkAchievement;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class WorkAchievementController(ExperienceContext context) : Controller
    {
        private readonly ExperienceContext _ctx = context;

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/work/{id}/achievement")]
        public async Task<IActionResult> CreateWorkAchievement([FromBody] WorkAchievementCreate dto)
        {
            WorkAchievement dbo = new()
            {
                WorkId = dto.WorkId,
                Description = dto.Description
            };

            await _ctx.WorkAchievement.AddAsync(dbo);
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        private WorkAchievementRead Convert(WorkAchievement x)
        {
            return new WorkAchievementRead()
            {
                Id = x.Id,
                WorkId = x.WorkId,
                Description = x.Description
            };
        }

        [HttpGet("/experience/work/achievement/{id}")]
        public async Task<IActionResult> GetWorkAchievementById(Guid id)
        {
            if (!_ctx.WorkAchievement.Any(dbo => dbo.Id == id)) return NotFound($"No such WorkAchievement with ID {id}");
            
            WorkAchievement dbo = await _ctx.WorkAchievement.FirstAsync(dbo => dbo.Id == id);
            return Ok(Convert(dbo));
        }

        [HttpGet("/experience/work/{id}/achievement")]
        public async Task<IActionResult> GetWorkAchievementsByWorkId(int id)
        {
            if (!_ctx.Work.Any(dbo => dbo.Id == id)) return NotFound($"No such Work with ID {id}");
            
            List<WorkAchievement> dbo = await _ctx.WorkAchievement.Where(dbo => dbo.WorkId == id).ToListAsync();
            return Ok(dbo.ConvertAll(Convert));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/work/achievement/{id}")]
        public async Task<IActionResult> UpdateWorkAchievement(Guid id, [FromBody] WorkAchievementUpdate dto)
        {
            if (!_ctx.WorkAchievement.Any(dbo => dbo.Id == id)) return NotFound($"No such WorkAchievement with ID {id}");
            
            WorkAchievement dbo = await _ctx.WorkAchievement.FirstAsync(dbo => dbo.Id == id);
            dbo.Description = dto.Description;

            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/achievement/{id}")]
        public async Task<IActionResult> DeleteWorkAchievement(Guid id)
        {
            if (!_ctx.WorkAchievement.Any(dbo => dbo.Id == id)) return NotFound($"No such WorkAchievement with ID {id}");

            await _ctx.WorkAchievement.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _ctx.SaveChangesAsync();

            return Ok();
        }
    }
}
