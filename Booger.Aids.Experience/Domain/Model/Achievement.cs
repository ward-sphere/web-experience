using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Achievement")]
    public class Achievement : BaseModel<int>
    {
        [Column(Order = 1)]
        [Required]
        public string Description { get; set; }
    }
}
