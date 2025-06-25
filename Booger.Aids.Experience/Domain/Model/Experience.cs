using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Experience")]
    public class Experience : BaseModel<int>
    {

        [Column(Order = 1)]
        [Required]
        public string Title { get; set; }

        [Column(Order = 2)]
        [Required]
        public string Organization { get; set; }

        [Column(Order = 3)]
        [Required]
        public DateOnly StartDate { get; set; }

        [Column(Order = 4)]
        public DateOnly? EndDate { get; set; } = null;

        [Column(Order = 5)]
        [Required]
        public string Location { get; set; }

        [Column(Order = 6)]
        [Required]
        public string Description { get; set; }

    }
}
