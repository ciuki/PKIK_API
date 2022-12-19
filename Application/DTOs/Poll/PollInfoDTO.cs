using Application.DTO.User;
using Application.DTOs.Question;
using Core.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.DTOs.Poll
{
    public class PollInfoDTO
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
        [SwaggerSchema("The boolean determining if the poll allows eligible to vote users to view results when true, or only moderators and creator if false")]
        public bool ResultsArePublic { get; set; }
        [SwaggerSchema("The enumerable determinig type of the poll")]
        public PollType PollType { get; set; }
        [SwaggerSchema("The date when the poll gets active, if null poll starts immediately", Format = "date-time")]
        public DateTime? StartDate { get; set; }
        [SwaggerSchema("The date when the poll gets deactivated, if null poll never ends", Format = "date-time")]
        public DateTime EndDate { get; set; }
        [SwaggerSchema("The list of users that allowed to vote")]
        public virtual ICollection<UserBaseDTO>? AllowedUsers { get; set; }
        [SwaggerSchema("The list of users that have moderator permissions")]
        public virtual ICollection<UserBaseDTO>? Moderators { get; set; }
        [SwaggerSchema("The list of voting tokens which are required to vote in protected poll")]
        public virtual ICollection<string>? VotingTokens { get; set; }
        [SwaggerSchema("The list of questions")]
        public virtual ICollection<QuestionBaseDTO> Questions { get; set; } = new List<QuestionBaseDTO>();
    }
}

