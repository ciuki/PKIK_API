using Application.DTOs.Poll;
using Core.Enums;
using FluentValidation;

namespace Application.Validators.Poll
{
    public class PollCreateValidator : AbstractValidator<PollCreateDTO>
    {
        public PollCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Questions).NotNull();
            RuleFor(x => x.Questions).Must(y => y.Any()).WithMessage("At least one question is reqiuerd").When(x => x.Questions is not null);
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now).When(x => x.StartDate is not null).WithMessage("Start date must be in future");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate.Value.AddMinutes(15)).When(x => x.StartDate is not null && x.EndDate is not null).WithMessage("The poll minimum length is 15 minutes");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(DateTime.Now.AddMinutes(15)).When(x => x.StartDate is null && x.EndDate is not null).WithMessage("The poll minimum length is 15 minutes");
            RuleFor(x => x.PollType).IsInEnum();
            RuleForEach(x => x.Questions).SetValidator(new QuestionCreateValidator()).When(x => x.Questions is not null); ;
            RuleFor(x => x.AllowAnonymous).Equal(false).When(x => x.PollType == PollType.Private || x.PollType == PollType.Protected).WithMessage("This poll type allows only non anonymous users");
        }

    }
}
