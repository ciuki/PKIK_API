using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class PollModerator
    {
        [Key, Column(Order = 1)]
        public int PollId { get; set; }
        public Poll Poll { get; set; }
        [Key, Column(Order = 2)]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
