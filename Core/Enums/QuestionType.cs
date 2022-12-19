
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Enums
{
    [SwaggerSchema("The enumerable determinig type of the question")]
    public enum QuestionType
    {
        Closed,
        Open,
        Emoji,
        Reaction
    }
}
