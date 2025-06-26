using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Experience")]
    public class Experience : BaseModel<int>
    {

        [Column]
        [Required]
        public string Title { get; set; }

        [Column]
        [Required]
        public string EmploymentType { get; set; }

        [Column]
        [Required]
        public string Organization { get; set; }

        [Column]
        [Required]
        public DateOnly StartDate { get; set; }

        [Column]
        public DateOnly? EndDate { get; set; } = null;

        [Column]
        [Required]
        public string Location { get; set; }

        [Column]
        [Required]
        public string Description { get; set; }
    }
}
