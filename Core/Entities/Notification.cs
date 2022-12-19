using Core.Entities.Base;

namespace Core.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool Seen { get; set; }
    }
}
