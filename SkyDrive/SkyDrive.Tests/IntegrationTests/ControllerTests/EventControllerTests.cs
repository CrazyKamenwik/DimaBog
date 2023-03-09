using FluentAssertions;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace SkyDrive.Tests.IntegrationTests.ControllerTests
{
    public class EventControllerTests : IClassFixture<TestHttpClientFactory<Program>>
    {
        private readonly HttpClient _client;

        public EventControllerTests(TestHttpClientFactory<Program> appFactory)
        {
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllEvents_StatusCodeOK()
        {
            //Arrange
            var response = await _client.GetAsync("Event");

            //Act
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<EventViewModel>>();

            //Assert
            result.Should().NotContainNulls();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetEventById_CorrectId_ReturnEventViewModel()
        {
            //Arrange
            var eventId = 1;

            //Act
            var response = await _client.GetAsync($"Event/{eventId}");
            var responseEventViewModel = await response.Content.ReadFromJsonAsync<EventViewModel>();

            //Assert
            responseEventViewModel.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetEventById_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var eventId = 92;

            //Act
            var response = await _client.GetAsync($"Event/{eventId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostEvent_CorrectEvent_ReturnOK(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 0;

            //Act
            var response = await _client.PostAsJsonAsync("Event", eventViewModel);
            var responseEventViewModel = await response.Content.ReadFromJsonAsync<EventViewModel>();

            //Assert
            responseEventViewModel.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostEvent_IncorrectEvent_ReturnInternalServerError(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 0;
            eventViewModel.DateTimeOfEvent = DateTime.Now.AddDays(-1);

            //Act
            var response = await _client.PostAsJsonAsync("Event", eventViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostEvent_EventWithTheSameIdAlreadyExist_ReturnBadRequest(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 5;

            //Act
            var response = await _client.PostAsJsonAsync("Event", eventViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutEvent_CorrectEvent_ReturnEventViewModel(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 6;

            //Act
            var response = await _client.PutAsJsonAsync("Event", eventViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutEvent_IncorrectEvent_ReturnInternalServerError(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 7;
            eventViewModel.DateTimeOfEvent = DateTime.Now.AddDays(-1);

            //Act   
            var response = await _client.PutAsJsonAsync("Event", eventViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutEvent_EventNotFound_ReturnNotFound(EventViewModel eventViewModel)
        {
            //Arrange
            eventViewModel.Id = 98;

            //Act
            var response = await _client.PutAsJsonAsync("Event", eventViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteEvent_CorrectId_ReturnOK()
        {
            //Arrange
            var eventId = 9;

            //Act
            var response = await _client.DeleteAsync($"Event/{eventId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteEvent_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var eventId = 100;

            //Act
            var response = await _client.DeleteAsync($"Event/{eventId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
