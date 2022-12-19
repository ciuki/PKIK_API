using Application.DTOs.Question;
using FluentValidation;

namespace Application.Validators.Poll
{
    public class QuestionCreateValidator : AbstractValidator<QuestionCreateDTO>
    {
        public QuestionCreateValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Answers).Must(x => !x.Any()).When(x => x.Type == Core.Enums.QuestionType.Open && x.Answers is not null).WithMessage("QuestionType Open requires no answers");
            RuleFor(x => x.Answers).NotNull().When(x => x.Type == Core.Enums.QuestionType.Closed);
            RuleFor(x => x.Answers).Must(x => x.Any()).When(x => x.Type == Core.Enums.QuestionType.Closed && x.Answers is not null).WithMessage("At least one answer is required when QuestionType is Closed");
            RuleForEach(x => x.Answers).SetValidator(new AnswerCreateValidator());
        }
    }
}
