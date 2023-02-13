using FluentValidation;
using SkyDrive.ViewModels;

namespace SkyDrive.Validators
{
    public class MemberValidator : AbstractValidator<MemberViewModel>
    {
        public MemberValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");

            RuleFor(m => m.MiddleName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");

            RuleFor(m => m.LastName)
                .NotEmpty().Length(2, 20).WithMessage("{PropertyName} has incorrect length");
        }
    }
}
