using Application.DTOs.User;
using FluentValidation;

namespace Application.Validators.User
{
    public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationDTO>
    {
        public EmailConfirmationValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ConfirmationToken).NotEmpty();
        }
    }
}
