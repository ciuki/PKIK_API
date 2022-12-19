using Application.DTOs.Poll;

namespace Application.Interfaces
{
    public interface IPollService
    {
        Task<IEnumerable<PollLiteDTO>> GetAll();
        Task<PollBaseDTO> Get(int pollId);
        Task<PollInfoDTO> GetInfo(int pollId);
        Task<IEnumerable<PollLiteDTO>> GetMyPolls();
        //Task<PollLiteDTO> Update(PollCreateDTO pollCreateDTO, int id);
        Task Delete(int pollId);
        Task<int> Create(PollCreateDTO pollCreateDTO);
        Task InviteUsers(int pollId, PollInviteDTO pollInviteDTO);
        Task SetPollModerators(int pollId, PollInviteDTO pollInviteDTO);
        Task OpenPoll(int pollId);
        Task ClosePoll(int pollId);
        Task OpenAllPolls();
        Task CloseAllPolls();

    }
}
