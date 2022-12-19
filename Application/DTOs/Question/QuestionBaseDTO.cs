using Application.DTOs.Answer;
using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Question
{
    public class QuestionBaseDTO
    {
        [SwaggerSchema("The question identifier")]
        public int Id { get; set; }
        [SwaggerSchema("The text of question", Nullable = false)]
        public string Text { get; set; } = string.Empty;
        [SwaggerSchema("The detailed description of question")]
        public string? Description { get; set; }
        [SwaggerSchema("The enumerable determinig type of the question(Closed, Open, Emoji, Reaction")]
        public QuestionType Type { get; set; }
        [SwaggerSchema("The list of answers")]
        public virtual ICollection<AnswerBaseDTO> Answers { get; set; } = new List<AnswerBaseDTO>();
    }
}
