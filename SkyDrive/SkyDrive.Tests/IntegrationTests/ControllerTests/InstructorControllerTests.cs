using FluentAssertions;
using SkyDrive.Tests.FixtureCustomization.Attributes;
using SkyDrive.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace SkyDrive.Tests.IntegrationTests.ControllerTests
{
    public class InstructorControllerTests : IClassFixture<TestHttpClientFactory<Program>>
    {
        private readonly HttpClient _client;

        public InstructorControllerTests(TestHttpClientFactory<Program> appFactory)
        {
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllInstructors_StatusCodeOK()
        {
            var response = await _client.GetAsync("Instructor");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<InstructorViewModel>>();

            result.Should().NotBeNullOrEmpty();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetInstructorById_CorrectId_ReturnInstructorViewModel()
        {
            //Arrange
            var instructorId = 1;

            //Act
            var response = await _client.GetAsync($"Instructor/{instructorId}");
            var responseInstructorViewModel = await response.Content.ReadFromJsonAsync<InstructorViewModel>();

            //Assert
            responseInstructorViewModel.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetInstructorById_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var instructorId = 92;

            //Act
            var response = await _client.GetAsync($"Instructor/{instructorId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostInstructor_CorrectInstructor_ReturnOK(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 93;

            //Act
            var response = await _client.PostAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.Content.ReadFromJsonAsync<InstructorViewModel>().Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostInstructor_IncorrectInstructor_ReturnBadRequest(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 4;
            instructorViewModel.LastName = null!;

            //Act
            var response = await _client.PostAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PostInstructor_InstructorWithTheSameIdAlreadyExist_ReturnBadRequest(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 5;

            //Act
            var response = await _client.PostAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutInstructor_CorrectInstructor_ReturnInstructorViewModel(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 6;

            //Act
            var response = await _client.PutAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutInstructor_IncorrectInstructor_ReturnBadRequest(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 7;
            instructorViewModel.MiddleName = null!;

            //Act   
            var response = await _client.PutAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [CorrectDataForValidators]
        public async Task PutInstructor_InstructorNotFound_ReturnNotFound(InstructorViewModel instructorViewModel)
        {
            //Arrange
            instructorViewModel.Id = 98;

            //Act
            var response = await _client.PutAsJsonAsync("Instructor", instructorViewModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteInstructor_CorrectId_ReturnOK()
        {
            //Arrange
            var instructorId = 9;

            //Act
            var response = await _client.DeleteAsync($"Instructor/{instructorId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteInstructor_IncorrectId_ReturnNotFound()
        {
            //Arrange
            var instructorId = 100;

            //Act
            var response = await _client.DeleteAsync($"Instructor/{instructorId}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
