using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("ExperienceSkill")]
    public class ExperienceSkill : BaseModel<Guid>
    {

        [Column]
        [ForeignKey("Experience")]
        [Required]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        [Column]
        [ForeignKey("Skill")]
        [Required]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
