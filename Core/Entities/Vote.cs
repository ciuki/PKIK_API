using Core.Entities.Base;

namespace Core.Entities
{
    public class Vote : BaseEntity
    {
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
