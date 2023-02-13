using FluentValidation;
using SkyDrive.ViewModels;

namespace SkyDrive.Validators
{
    public class EventValidator : AbstractValidator<EventViewModel>
    {
        public EventValidator()
        {
            RuleFor(e => e.DateTimeOfEvent)
                .NotEmpty().GreaterThan(DateTime.Now);
        }
    }
}
