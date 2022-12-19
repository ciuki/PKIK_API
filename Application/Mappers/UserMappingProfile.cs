using Application.DTO.User;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserBaseDTO>().PreserveReferences();
        }
    }
}
