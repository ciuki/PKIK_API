using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class VoteSummaryDTO
    {
        [SwaggerSchema("The question identificator")]
        public int QuestionId { get; set; }
        [SwaggerSchema("The question text", Nullable = false)]
        public string QuestionText { get; set; } = string.Empty;
        [SwaggerSchema("The enumerable determinig type of the question(Closed, Open, Emoji, Reaction")]
        public QuestionType QuestionType { get; set; }
        [SwaggerSchema("The list of answer voting summary")]
        public ICollection<AnswerVotingSummaryDTO>? Answers { get; set; }
    }
}
