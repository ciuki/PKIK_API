using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTO
{
    public class UserCreateDTO
    {
        [SwaggerSchema("The user email", Nullable = false)]
        public string Email { get; set; } = string.Empty;
        [SwaggerSchema("The user password", Nullable = false, Format = "password")]
        public string Password { get; set; } = string.Empty;
        [SwaggerSchema("The user first name")]
        public string? FirstName { get; set; }
        [SwaggerSchema("The user last name")]
        public string? LastName { get; set; }
    }
}
