using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Answer
{
    public class AnswerCreateDTO
    {
        [SwaggerSchema("The answer text", Nullable = false)]
        public string Text { get; set; } = string.Empty;
    }
}