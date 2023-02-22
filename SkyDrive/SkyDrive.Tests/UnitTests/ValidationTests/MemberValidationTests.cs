using FluentAssertions;
using FluentValidation;
using SkyDrive.Tests.FixtureCustomization;
using SkyDrive.Validators;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.ValidationTests
{
    public class MemberValidationTests
    {
        private readonly IValidator<MemberViewModel> _memberValidator;

        public MemberValidationTests()
        {
            _memberValidator = new MemberValidator();
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task MemberValidator_CorrectData_ValidationTrue(MemberViewModel memberViewModel)
        {
            //Act
            var result = await _memberValidator.ValidateAsync(memberViewModel);

            //Assert
            result.IsValid.Should().Be(true);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task MemberValidator_IncorrectFirstName_ValidationFalse(
            MemberViewModel memberViewModel,
            string firstName)
        {
            //Arrange
            memberViewModel.FirstName = firstName;

            //Act
            var result = await _memberValidator.ValidateAsync(memberViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task MemberValidator_IncorrectMiddleName_ValidationFalse(
            MemberViewModel memberViewModel,
            string middleName)
        {
            //Arrange
            memberViewModel.MiddleName = middleName;

            //Act
            var result = await _memberValidator.ValidateAsync(memberViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task MemberValidator_IncorrectLastName_ValidationFalse(
            MemberViewModel memberViewModel,
            string lastName)
        {
            //Arrange
            memberViewModel.MiddleName = lastName;

            //Act
            var result = await _memberValidator.ValidateAsync(memberViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }
    }
}
