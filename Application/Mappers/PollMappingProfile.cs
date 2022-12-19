using Application.DTOs.Poll;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class PollMappingProfile : Profile
    {
        public PollMappingProfile()
        {
            CreateMap<PollCreateDTO, Poll>()
                .ForMember(dest => dest.AllowedUsers,
                opt => opt.MapFrom(
                    source => source.AllowedUsersIds.Select(id => new PollAllowed { UserId = id })))
                .ForMember(dest => dest.Moderators,
                opt => opt.MapFrom(
                    source => source.ModeratorsIds.Select(id => new PollModerator { UserId = id })))
                .PreserveReferences();
            CreateMap<Poll, PollBaseDTO>()
                .PreserveReferences();
            CreateMap<Poll, PollLiteDTO>().PreserveReferences();
            CreateMap<Poll, PollInfoDTO>().PreserveReferences()
                .ForMember(dest => dest.Moderators, opt => opt.MapFrom(so => so.Moderators.Select(t => t.User).ToList()))
                .ForMember(dest => dest.AllowedUsers, opt => opt.MapFrom(so => so.AllowedUsers.Select(t => t.User).ToList()))
                .ForMember(dest => dest.VotingTokens, opt => opt.MapFrom(so => so.VotingTokens.Select(t => t.Token).ToList()));
        }
    }
}
