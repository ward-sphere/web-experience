using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.WorkAchievement;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class WorkAchievementController(ExperienceContext context) : Controller
    {
        private readonly ExperienceContext _ctx = context;

        private async Task ValidateWork(int id)
        {
            if (await _ctx.Work.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such Work with ID {id}")
            };
        }

        private async Task ValidateWorkAchievement(Guid id)
        {
            if (await _ctx.WorkAchievement.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such Work with ID {id}")
            };
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("/experience/work/{workId}/achievement")]
        public async Task<IActionResult> CreateWorkAchievement(int workId, [FromBody] WorkAchievementCreate dto)
        {
            try { await ValidateWork(workId); }
            catch (ValidationFailedException e) { return e.Result; }

            WorkAchievement dbo = new()
            {
                WorkId = workId,
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
            try { await ValidateWorkAchievement(id); }
            catch (ValidationFailedException e) { return e.Result; }
            
            WorkAchievement dbo = await _ctx.WorkAchievement.FirstAsync(dbo => dbo.Id == id);
            return Ok(Convert(dbo));
        }

        [HttpGet("/experience/work/{id}/achievement")]
        public async Task<IActionResult> GetWorkAchievementsByWorkId(int id)
        {
            try { await ValidateWork(id); }
            catch (ValidationFailedException e) { return e.Result; }
            
            List<WorkAchievement> dbo = await _ctx.WorkAchievement.Where(dbo => dbo.WorkId == id).ToListAsync();
            return Ok(dbo.ConvertAll(Convert));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/experience/work/achievement/{id}")]
        public async Task<IActionResult> UpdateWorkAchievement(Guid id, [FromBody] WorkAchievementUpdate dto)
        {
            try { await ValidateWorkAchievement(id); }
            catch (ValidationFailedException e) { return e.Result; }

            WorkAchievement dbo = await _ctx.WorkAchievement.FirstAsync(dbo => dbo.Id == id);
            dbo.Description = dto.Description;

            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/achievement/{id}")]
        public async Task<IActionResult> DeleteWorkAchievement(Guid id)
        {
            try { await ValidateWorkAchievement(id); }
            catch (ValidationFailedException e) { return e.Result; }

            await _ctx.WorkAchievement.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _ctx.SaveChangesAsync();

            return Ok();
        }
    }
}
