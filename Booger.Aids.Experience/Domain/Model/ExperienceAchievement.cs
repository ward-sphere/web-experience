using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("ExperienceAchievement")]
    public class ExperienceAchievement : BaseModel<Guid>
    {
        [Column]
        [Required]
        [ForeignKey("Experience")]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        [Column]
        [Required]
        public string Description { get; set; }
    }
}
