using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Poll
{
    public class PollInviteDTO
    {
        [SwaggerSchema("The list of vote users ids")]
        public virtual ICollection<string>? UserIds { get; set; }
    }
}
