using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Service.Infrastructure
{
    public class ExperienceContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                System.Environment.GetEnvironmentVariable("PORTFOLIO_PGSQL_CONNECTION_STRING")
            );
        }


        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ExperienceAchievement> ExperienceAchievements { get; set; }
        public DbSet<ExperienceSkill> ExperienceSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
