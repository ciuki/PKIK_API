using Application.DTOs.Vote;

namespace Application.Interfaces
{
    public interface IVoteService
    {
        Task VoteAggregate(VoteAggregateDTO votes);
        Task<VoteInfoDTO> Get(int pollId);
    }
}
