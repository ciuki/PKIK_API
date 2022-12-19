using Application.DTOs.Notification;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<NotificationCreateDTO, Notification>().PreserveReferences();
            CreateMap<Notification, NotificationBaseDTO>().PreserveReferences();
            CreateMap<Notification, NotificationLiteDTO>().PreserveReferences();
        }
    }
}
