using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    [Table("Education")]
    public class Education : Experience
    {

        [Column(Order = 7)]
        public string? Field { get; set; } = null;

    }
}
