using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("WorkAchievement")]
    public class WorkAchievement : BaseModel<Guid>
    {
        [Column]
        [Required]
        [ForeignKey("Work")]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [Column]
        [Required]
        public string Description { get; set; }
    }
}
