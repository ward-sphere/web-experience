using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Education")]
    public class Education : BaseModel<int>
    {
        [Column]
        [Required]
        public string School { get; set; }

        [Column]
        [Required]
        public string Degree { get; set; }

        [Column]
        public string? Field { get; set; } = null;

        [Column]
        [Required]
        public DateOnly StartDate { get; set; }

        [Column]
        public DateOnly? EndDate { get; set; } = null;

        [Column]
        [Required]
        public string Description { get; set; }

    }
}
