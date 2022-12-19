using Core.Entities;
using Core.Specifications.Base;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class PollSpecification : BaseSpecification<Poll>
    {
        public PollSpecification(Expression<Func<Poll, bool>> criteria)
        : base(criteria)
        {
            AddInclude(x => x.AllowedUsers);
            AddInclude(x => x.Moderators);
            AddInclude(x => x.Questions);
            AddInclude(x => x.VotingTokens);
            AddInclude($"{nameof(Poll.AllowedUsers)}.{nameof(PollAllowed.User)}");
            AddInclude($"{nameof(Poll.Moderators)}.{nameof(PollAllowed.User)}");
            AddInclude($"{nameof(Poll.Questions)}.{nameof(Question.Answers)}");
        }
    }
}
