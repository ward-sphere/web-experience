using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Skill")]
    public class Skill : BaseModel<int>
    {

        [Column(Order = 1)]
        [Required]
        public string Name { set; get; }

    }
}
