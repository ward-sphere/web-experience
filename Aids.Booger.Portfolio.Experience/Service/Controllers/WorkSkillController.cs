using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dto.WorkSkill;
using Service.Infrastructure;

namespace Service.Controllers
{
    [ApiController]
    public class WorkSkillController(ExperienceContext context) : Controller
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

        private async Task ValidateSkill(int id)
        {
            if (await _ctx.Skill.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such Skill with ID {id}")
            };
        }

        private async Task ValidateWorkSkill(int workId, int skillId)
        {
            if (await _ctx.WorkSkill.AnyAsync(dbo 
                => dbo.WorkId == workId && dbo.SkillId == skillId)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"Work {workId} is not currently associated with Skill {skillId}")
            };
        }

        private async Task ValidateWorkSkill(Guid id)
        {
            if (await _ctx.WorkSkill.AnyAsync(dbo => dbo.Id == id)) return;

            throw new ValidationFailedException()
            {
                Result = NotFound($"No such WorkSkill with ID {id}")
            };
        }

        [Authorize("AdminOnly")]
        [HttpPost("/experience/work/{workId}/skill/{skillId}")]
        public async Task<IActionResult> CreateWorkSkill(int workId, int skillId)
        {
            try
            {
                await ValidateWork(workId);
                await ValidateSkill(skillId);
            }
            catch (ValidationFailedException e) { return e.Result; }

            if (await _ctx.WorkSkill.AnyAsync(dbo => dbo.WorkId == workId && dbo.SkillId == skillId))
                return Conflict($"Work {workId} already associated with Skill {skillId}");
            
            WorkSkill dbo = new()
            {
                WorkId = workId,
                SkillId = skillId
            };

            await _ctx.WorkSkill.AddAsync(dbo);
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        private WorkSkillRead Convert(WorkSkill dbo)
        {
            return new WorkSkillRead()
            {
                Id = dbo.Id,
                WorkId = dbo.WorkId,
                SkillId = dbo.SkillId
            };
        }

        [HttpGet("/experience/work/{workId}/skill")]
        public async Task<IActionResult> GetAllSkillsByWorkId(int workId)
        {
            try
            {
                await ValidateWork(workId);
            }
            catch (ValidationFailedException e) { return e.Result; }

            List<WorkSkill> dbo = await _ctx.WorkSkill.Where(dbo => dbo.WorkId == workId).ToListAsync();
            return Ok(dbo.ConvertAll(Convert));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/{workId}/skill/{skillId}")]
        public async Task<IActionResult> RemoveSkillFromWork(int workId, int skillId)
        {
            try
            {
                await ValidateWork(workId);
                await ValidateSkill(skillId);
                await ValidateWorkSkill(workId, skillId);
            }
            catch (ValidationFailedException e) { return e.Result; }

            await _ctx.WorkSkill
                .Where(dbo => dbo.WorkId == workId && dbo.SkillId == skillId)
                .ExecuteDeleteAsync();
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("/experience/work/skill/{id}")]
        public async Task<IActionResult> RemoveSkillById(Guid id)
        {
            try { await ValidateWorkSkill(id); }
            catch (ValidationFailedException e) { return e.Result; }

            await _ctx.WorkSkill.Where(dbo => dbo.Id == id).ExecuteDeleteAsync();
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        private class ValidationFailedException : Exception { public IActionResult Result { get; set; } }
    }
}
