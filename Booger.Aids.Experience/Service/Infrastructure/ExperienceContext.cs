using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Service.Infrastructure
{
    public class ExperienceContext(DbContextOptions<ExperienceContext> options) : DbContext(options)
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ExperienceAchievement> ExperienceAchievements { get; set; }
        public DbSet<ExperienceSkill> ExperienceSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
