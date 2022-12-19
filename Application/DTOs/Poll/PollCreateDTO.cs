using Application.DTOs.Question;
using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Poll
{
    public class PollCreateDTO
    {
        [SwaggerSchema("The poll name", Nullable = false)]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("The boolean determining if the poll allows voting without login when true")]
        public bool AllowAnonymous { get; set; }
        [SwaggerSchema("The boolean determining if the poll allows eligible to vote users to view results when true, or only moderators and creator if false")]
        public bool ResultsArePublic { get; set; }
        [SwaggerSchema("The enumerable determinig type of the poll")]
        public PollType PollType { get; set; }
        [SwaggerSchema("The date when the poll gets active, if null poll starts immediately", Format = "date-time")]
        public DateTime? StartDate { get; set; }
        [SwaggerSchema("The date when the poll gets deactivated, if null poll never ends", Format = "date-time")]
        public DateTime? EndDate { get; set; }
        [SwaggerSchema("The list of questions")]
        public virtual ICollection<QuestionCreateDTO> Questions { get; set; } = new List<QuestionCreateDTO>();
        [SwaggerSchema("The list of allowed to vote users ids")]
        public virtual ICollection<string>? AllowedUsersIds { get; set; }
        [SwaggerSchema("The list of users ids that have moderator permissions")]
        public virtual ICollection<string>? ModeratorsIds { get; set; }
    }
}
