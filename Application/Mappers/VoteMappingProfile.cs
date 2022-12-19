using Application.DTOs.Vote;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<Vote, VoteBaseDTO>().PreserveReferences()
                .ForMember(dest => dest.Question, opt =>
                    opt.MapFrom(src => src.Question.Text))
                .ForMember(dest => dest.Answer, opt =>
                    opt.MapFrom(src => src.Answer.Text));
            CreateMap<VoteCreateDTO, Vote>().PreserveReferences()
                .ForMember(dest => dest.Answer, opt =>
                 {
                     opt.PreCondition(src => src.AnswerText is not null);
                     opt.MapFrom(src => new Answer() { Text = src.AnswerText, QuestionId = src.QuestionId });
                 });
        }
    }
}
