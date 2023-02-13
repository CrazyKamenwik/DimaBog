using FluentValidation;
using SkyDrive.ViewModels;

namespace SkyDrive.Validators
{
    public class InstructorValidator : AbstractValidator<InstructorViewModel>
    {
        public InstructorValidator()
        {
            RuleFor(i => i.FirstName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");

            RuleFor(i => i.MiddleName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");

            RuleFor(i => i.LastName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");

            RuleFor(i => i.Experience)
                .GreaterThan(0).WithMessage("{PropertyName} is incorrect");
        }
    }
}
