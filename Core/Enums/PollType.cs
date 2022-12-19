using Swashbuckle.AspNetCore.Annotations;

namespace Core.Enums
{
    [SwaggerSchema("The enumerable determinig type of the poll")]
    public enum PollType
    {
        Private,
        Public,
        Protected,
        Hidden
    }
}
