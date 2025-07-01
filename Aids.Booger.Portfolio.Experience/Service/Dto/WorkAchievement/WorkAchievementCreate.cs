using System.ComponentModel.DataAnnotations;

namespace Service.Dto.WorkAchievement
{
    public class WorkAchievementCreate
    {
        public int WorkId { get; set; }
        [MaxLength(325)]
        public string Description { get; set; }
    }
}
