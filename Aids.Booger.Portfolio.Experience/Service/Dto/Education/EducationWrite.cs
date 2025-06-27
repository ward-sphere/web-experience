using System.ComponentModel.DataAnnotations;

namespace Service.Dto.Education
{
    public class EducationWrite
    {
        [Required]
        public string School { get; set; }
        [Required]
        public string Degree { get; set; }
        public string? Field { get; set; } = null;
        [Required]
        public string StartDate { get; set; }
        public string? EndDate { get; set; } = null;
        [Required]
        public string Description { get; set; }
    }
}
