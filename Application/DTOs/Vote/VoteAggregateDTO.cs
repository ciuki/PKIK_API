using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class VoteAggregateDTO
    {
        [SwaggerSchema("The poll id for which the vote is being made")]
        public int PollId { get; set; }
        [SwaggerSchema("The voting token required for protedted poll")]
        public string? VotingToken { get; set; }
        [SwaggerSchema("The list of votes")]
        public ICollection<VoteCreateDTO>? Votes { get; set; }
    }
}
