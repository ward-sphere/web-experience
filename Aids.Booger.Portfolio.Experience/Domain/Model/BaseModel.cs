using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class BaseModel<T>
    {

        [Column(Order = 0)]
        [Required]
        public T Id { get; set; }

    }
}
