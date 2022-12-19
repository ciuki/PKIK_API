using Application.DTOs.Answer;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class AnswerMappingProfile : Profile
    {
        public AnswerMappingProfile()
        {
            CreateMap<AnswerCreateDTO, Answer>().PreserveReferences();
            CreateMap<Answer, AnswerBaseDTO>().PreserveReferences();
        }
    }
}
