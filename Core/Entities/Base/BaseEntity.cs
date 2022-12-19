using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
