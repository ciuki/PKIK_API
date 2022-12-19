using Application.DTOs.Answer;
using FluentValidation;

namespace Application.Validators.Poll
{
    public class AnswerCreateValidator : AbstractValidator<AnswerCreateDTO>
    {
        public AnswerCreateValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
