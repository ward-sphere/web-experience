namespace Service.Dto.Work
{
    public class WorkWrite
    {
        public string Title { get; set; }
        public string EmploymentType { get; set; }
        public string Organization { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; } = null;
        public string Description { get; set; }
        public Location Location { get; set; }
    }
}
