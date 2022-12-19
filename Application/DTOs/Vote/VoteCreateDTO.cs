using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Vote
{
    public class VoteCreateDTO
    {
        [SwaggerSchema("The question identificator")]
        public int QuestionId { get; set; }
        [SwaggerSchema("The answer identificator used for all question types other than open")]
        public int? AnswerId { get; set; }
        [SwaggerSchema("The answer text used for open question type")]
        public string? AnswerText { get; set; }
    }
}
