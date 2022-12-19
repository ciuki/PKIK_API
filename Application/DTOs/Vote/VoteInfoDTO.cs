using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class VoteInfoDTO
    {
        [SwaggerSchema("The list of list of all submitted votes summary for selected poll")]
        public ICollection<VoteSummaryDTO>? VoteQuestions { get; set; }
        [SwaggerSchema("The list of list of all submitted votes for selected poll")]
        public ICollection<VoteBaseDTO>? BaseAnswers { get; set; }
    }
}
