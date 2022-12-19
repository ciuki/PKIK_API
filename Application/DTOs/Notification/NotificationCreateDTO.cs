using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Notification
{
    public class NotificationCreateDTO
    {
        [SwaggerSchema("The notification recieving user identifier", Nullable = false)]
        public string UserId { get; set; } = string.Empty;
        [SwaggerSchema("The notification title", Nullable = false)]
        public string Title { get; set; } = string.Empty;
        [SwaggerSchema("The notification detailed message", Nullable = false)]
        public string Message { get; set; } = string.Empty;
    }
}
