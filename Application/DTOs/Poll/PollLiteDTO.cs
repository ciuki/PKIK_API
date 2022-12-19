using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Poll
{
    public class PollLiteDTO
    {
        [SwaggerSchema("The poll identifier")]
        public int Id { get; set; }
        [SwaggerSchema("The poll name", Nullable = false)]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("The identifier of user that created poll", Nullable = false)]
        public string? CreatedBy { get; set; }
        [SwaggerSchema("The boolean determining if the poll allows voting without login when true")]
        public bool AllowAnonymous { get; set; }
        [SwaggerSchema("The boolean determining if the poll is active(allows voting) when true")]
        public bool IsActive { get; set; }
        [SwaggerSchema("The date when the poll gets active, if null poll starts immediately", Format = "date-time")]
        public DateTime StartDate { get; set; }
        [SwaggerSchema("The date when the poll gets deactivated, if null poll never ends", Format = "date-time")]
        public DateTime EndDate { get; set; }

    }
}
