using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Notification
{
    public class NotificationLiteDTO
    {
        [SwaggerSchema("The notification identifier")]
        public int Id { get; set; }
        [SwaggerSchema("The notification title", Nullable = false)]
        public string Title { get; set; } = string.Empty;
        [SwaggerSchema("The boolean determining if the notification was seen when true")]
        public bool Seen { get; set; }
    }
}
