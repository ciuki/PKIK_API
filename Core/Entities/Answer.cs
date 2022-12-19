using Core.Entities.Base;

namespace Core.Entities
{
    public class Answer : BaseEntity
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}
