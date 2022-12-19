using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class VotingToken
    {
        public int PollId { get; set; }
        [Key]
        public string Token { get; set; }
    }
}
