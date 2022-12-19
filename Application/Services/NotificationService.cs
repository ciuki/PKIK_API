using Application.DTOs.Notification;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(IMapper mapper, IHttpContextAccessor contextAccessor, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            this._notificationRepository = notificationRepository;
        }

        public async Task<int> Create(NotificationCreateDTO notificationCreateDTO)
        {
            Notification newNotification = _mapper.Map<Notification>(notificationCreateDTO);
            await _notificationRepository.AddAsync(newNotification);
            return newNotification.Id;
        }

        public async Task Delete(int notificationId)
        {
            string? userId = GetCurrentUserId();
            Notification pollToDelete = await _notificationRepository.GetByIdAsync(notificationId);
            if (pollToDelete is null)
            {
                throw new ObjectNotFoundException("This notification does not exist");
            }
            if (pollToDelete.UserId != userId)
            {
                throw new AccessForbiddenException("You do not have permissions to delete this notification");
            }
            await _notificationRepository.DeleteAsync(pollToDelete);
        }

        public async Task<NotificationBaseDTO> Get(int notificationId)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            Notification notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification is null)
            {
                throw new ObjectNotFoundException("This notification does not exist");
            }
            if (notification.UserId != userId)
            {
                throw new AccessForbiddenException("You do not have access to this notification");
            }
            NotificationBaseDTO result = _mapper.Map<NotificationBaseDTO>(notification);
            return result;
        }

        public async Task<NotificationBaseDTO> GetPrivate(int notificationId)
        {
            Notification notification = await _notificationRepository.GetByIdAsync(notificationId);
            NotificationBaseDTO result = _mapper.Map<NotificationBaseDTO>(notification);
            return result;
        }

        public async Task<IEnumerable<NotificationLiteDTO>> GetAll()
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            IEnumerable<Notification> notifications = await _notificationRepository.GetAllAsync();
            notifications = notifications.Where(x => x.UserId == userId);
            IEnumerable<NotificationLiteDTO> result = _mapper.Map<IEnumerable<NotificationLiteDTO>>(notifications);
            return result;
        }

        public async Task SetAsSeen(int notificationId)
        {
            string? userId = GetCurrentUserId();
            if (userId is null)
            {
                throw new AccessForbiddenException("You must be logged in");
            }
            Notification notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification is null)
            {
                throw new ObjectNotFoundException("This notification does not exist");
            }
            if (notification.UserId != userId)
            {
                throw new AccessForbiddenException("You do not have access to this notification");
            }
            notification.Seen = true;
            await _notificationRepository.UpdateAsync(notification);
        }

        private string? GetCurrentUserId()
        {
            var identity = (ClaimsIdentity?)_contextAccessor.HttpContext?.User.Identity;
            string? userId = null;
            if (identity is not null && identity.IsAuthenticated)
            {
                userId = identity.Claims?.FirstOrDefault(
                    x => x.Type.Contains("nameidentifier")
                    )?.Value;
            }
            return userId;
        }
    }
}
