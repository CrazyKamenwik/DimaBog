using FluentAssertions;
using FluentValidation;
using SkyDrive.Tests.FixtureCustomization;
using SkyDrive.Validators;
using SkyDrive.ViewModels;

namespace SkyDrive.Tests.UnitTests.ValidationTests
{
    public class EventValidationTests
    {
        private readonly IValidator<EventViewModel> _eventValidator;

        public EventValidationTests()
        {
            _eventValidator = new EventValidator();
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task EventValidator_CorrectData_ValidationTrue(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.DateTimeOfEvent = DateTime.Now.AddMilliseconds(1);

            //Act
            var result = await _eventValidator.ValidateAsync(eventViewModel);

            //Assert
            result.IsValid.Should().Be(true);
        }

        [Theory]
        [FixtureWithoutCirculation]
        public async Task EventValidator_IncorrectData_ValidationFalse(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.DateTimeOfEvent = DateTime.Now.AddDays(-1);

            //Act
            var result = await _eventValidator.ValidateAsync(eventViewModel);

            //Assert
            result.IsValid.Should().Be(false);
        }
    }
}
