using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Service.Infrastructure
{
    public class ExperienceContext(DbContextOptions<ExperienceContext> options) : DbContext(options)
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Work> Experiences { get; set; }
        public DbSet<WorkAchievement> ExperienceAchievements { get; set; }
        public DbSet<WorkSkill> ExperienceSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
