using Application.DTOs.Notification;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationLiteDTO>> GetAll();
        Task<NotificationBaseDTO> Get(int notificationId);
        Task<NotificationBaseDTO> GetPrivate(int notificationId);
        Task Delete(int notificationId);
        Task<int> Create(NotificationCreateDTO notificationCreateDTO);
        Task SetAsSeen(int notificationId);

    }
}
