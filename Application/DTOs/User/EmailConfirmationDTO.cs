using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.User
{
    public class EmailConfirmationDTO
    {
        [SwaggerSchema("The uri encoded user identificator", Nullable = false)]
        public string UserId { get; set; } = string.Empty;
        [SwaggerSchema("The uri encoded account confirmation token", Nullable = false)]
        public string? ConfirmationToken { get; set; } = string.Empty;
    }
}
