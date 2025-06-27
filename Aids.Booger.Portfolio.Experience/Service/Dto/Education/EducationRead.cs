using System.ComponentModel.DataAnnotations;

namespace Service.Dto.Education
{
    public class EducationRead
    {
        public int Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string? Field { get; set; } = null;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; } = null;
        public string Description { get; set; }
    }
}
