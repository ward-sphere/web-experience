using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("ExperienceSkill")]
    public class ExperienceSkill : BaseModel<Guid>
    {

        [Column(Order = 1)]
        [ForeignKey("Experience")]
        [Required]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        [Column(Order = 2)]
        [ForeignKey("Skill")]
        [Required]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
