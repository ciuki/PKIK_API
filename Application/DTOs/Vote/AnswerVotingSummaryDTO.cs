using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class AnswerVotingSummaryDTO
    {
        [SwaggerSchema("The answer identificator")]
        public int AnswerId { get; set; }
        [SwaggerSchema("The answer text", Nullable = false)]
        public string AnswerText { get; set; } = string.Empty;
        [SwaggerSchema("The number of votes submitted for a given answer")]
        public int Count { get; set; }
    }
}
