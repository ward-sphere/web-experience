namespace Service.Dto.Work
{
    public class WorkRead
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EmploymentType { get; set; }
        public string Organization { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
    }
}
