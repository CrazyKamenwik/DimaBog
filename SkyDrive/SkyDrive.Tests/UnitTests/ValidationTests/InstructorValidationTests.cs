using FluentAssertions;
using FluentValidation;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.Validators;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.ValidationTests
{
    public class InstructorValidationTests
    {
        private readonly IValidator<InstructorViewModel> _instructorValidator;

        public InstructorValidationTests()
        {
            _instructorValidator = new InstructorValidator();
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task InstructorValidator_CorrectData_ValidationTrue(InstructorViewModel instructorViewModel)
        {
            //Act
            var result = await _instructorValidator.ValidateAsync(instructorViewModel);

            //Assert
            result.IsValid.Should().Be(true);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task InstructorValidator_IncorrectFirstName_ValidationFalse(
            InstructorViewModel instructorViewModel,
            string firstName)
        {
            //Arrange
            instructorViewModel.FirstName = firstName;

            //Act
            var result = await _instructorValidator.ValidateAsync(instructorViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task InstructorValidator_IncorrectMiddleName_ValidationFalse(
            InstructorViewModel instructorViewModel,
            string middleName)
        {
            //Arrange
            instructorViewModel.MiddleName = middleName;

            //Act
            var result = await _instructorValidator.ValidateAsync(instructorViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task InstructorValidator_IncorrectLastName_ValidationFalse(
            InstructorViewModel instructorViewModel,
            string lastName)
        {
            //Arrange
            instructorViewModel.LastName = lastName;

            //Act
            var result = await _instructorValidator.ValidateAsync(instructorViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }
    }
}
