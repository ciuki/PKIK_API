using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTO
{
    public class LoginDTO
    {
        [SwaggerSchema("The user email", Nullable = false)]
        public string Email { get; set; } = string.Empty;
        [SwaggerSchema("The user password", Nullable = false, Format = "password")]
        public string Password { get; set; } = string.Empty;

    }
}
