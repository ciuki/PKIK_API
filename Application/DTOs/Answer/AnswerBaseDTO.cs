using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Answer
{
    public class AnswerBaseDTO
    {
        [SwaggerSchema("The answer identifier")]
        public int Id { get; set; }
        [SwaggerSchema("The answer text", Nullable = false)]
        public string Text { get; set; } = string.Empty;
    }
}
