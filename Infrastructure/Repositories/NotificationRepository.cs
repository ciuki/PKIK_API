using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, Core.Repositories.INotificationRepository
    {
        public NotificationRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
