using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Service.Infrastructure
{
    public class ExperienceContext(DbContextOptions<ExperienceContext> options) : DbContext(options)
    {
        public DbSet<Education> Education { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<WorkAchievement> WorkAchievement { get; set; }
        public DbSet<WorkSkill> WorkSkill { get; set; }
        public DbSet<Skill> Skill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasIndex(dbo => dbo.Name)
                .IsUnique(true);

            modelBuilder.Entity<WorkSkill>()
                .HasIndex(dbo => new { dbo.WorkId, dbo.SkillId })
                .IsUnique(true);
        }
    }
}
