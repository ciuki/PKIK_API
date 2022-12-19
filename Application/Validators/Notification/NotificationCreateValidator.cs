using Application.DTOs.Notification;
using FluentValidation;

namespace Application.Validators.Notification
{
    public class NotificationCreateValidator : AbstractValidator<NotificationCreateDTO>
    {
        public NotificationCreateValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
