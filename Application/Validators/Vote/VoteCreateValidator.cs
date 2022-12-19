using Application.DTOs.Vote;
using FluentValidation;

namespace Application.Validators.Vote
{
    public class VoteCreateValidator : AbstractValidator<VoteCreateDTO>
    {
        public VoteCreateValidator()
        {
            RuleFor(x => x.AnswerText).NotEmpty().When(x => x.AnswerId is null).WithMessage("AnswerText or AnswerId must not be null.");
            RuleFor(x => x.AnswerText).Null().When(x => x.AnswerId is not null);
            RuleFor(x => x.AnswerId).Null().When(x => x.AnswerText is not null);
        }
    }
}
