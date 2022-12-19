using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public string? Description { get; set; }
        public QuestionType Type { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public Poll Poll { get; set; }
        public int PollId { get; set; }
    }
}
