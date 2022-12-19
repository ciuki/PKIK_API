using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTO
{
    public class TokenInfoDTO
    {
        [SwaggerSchema("The JWT token", Nullable = false)]
        public string Token { get; set; } = string.Empty;
        [SwaggerSchema("The date when JWT token expires", Format = "date-time")]
        public DateTime TokenValidUntil { get; set; }
    }
}
