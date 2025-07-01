using System.ComponentModel.DataAnnotations;

namespace Service.Dto.WorkAchievement
{
    public class WorkAchievementUpdate
    {
        [MaxLength(325)]
        public string Description { get; set; }
    }
}
