using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class VoteBaseDTO
    {
        [SwaggerSchema("The enumerable determinig type of the question(Closed, Open, Emoji, Reaction")]
        public QuestionType QuestionType { get; set; }
        [SwaggerSchema("The user which voted identificator", Nullable = false)]
        public string UserId { get; set; } = string.Empty;
        [SwaggerSchema("The question identificator")]
        public int QuestionId { get; set; }
        [SwaggerSchema("The question text", Nullable = false)]
        public string Question { get; set; } = string.Empty;
        [SwaggerSchema("The answer identificator")]
        public int AnswerId { get; set; }
        [SwaggerSchema("The answer text", Nullable = false)]
        public string Answer { get; set; } = string.Empty;
    }
}
