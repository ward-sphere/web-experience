using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("WorkSkill")]
    public class WorkSkill : BaseModel<Guid>
    {

        [Column]
        [ForeignKey("Work")]
        [Required]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column]
        [ForeignKey("Skill")]
        [Required]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
