using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("ExperienceAchievement")]
    public class ExperienceAchievement : BaseModel<Guid>
    {
        [Column(Order = 1)]
        [Required]
        [ForeignKey("Experience")]
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Description { get; set; }
    }
}
